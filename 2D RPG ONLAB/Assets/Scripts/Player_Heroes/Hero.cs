using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EventCallbacks
{
    public abstract class Hero : MonoBehaviour
    {
        public Camera m_HeroCamera;

        public Image m_ImageCooldownBasic;
        public Image m_ImageCooldownSpellOne;

        public Text m_TextCooldownBasic;
        public Text m_TextCooldownSpellOne;

        public float m_SpellOneCooldown;
        protected float spellOneCooldownATM;
        public float m_SpellOneManaCost;


        public float m_BasicAttackCooldown;
        protected float basicAttackCooldownATM;


        protected Vector2 m_NormalizedMovement;

        public Slider m_HpSlider;
        public Slider m_ManaSlider;
        protected Color m_FullHealthColor = Color.green;
        protected Color m_ZeroHealthColor = Color.red;
        protected Color m_FullManaColor = Color.blue;
        protected Color m_ZeroManaColor = Color.grey;
        public Image m_FillHPImage;
        public Image m_FillManaImage;


        public Slot m_HelmetSlot;
        public Slot m_ArmorSlot;
        public Slot m_GlovesSlot;
        public Slot m_LeggingSlot;
        public Slot m_WeaponSlot;


        public Text m_StatText;

        protected new Rigidbody2D rigidbody;
        public GameObject m_FirstInventorySlot;
        [HideInInspector] public Inventory inventory;

        public int m_Lvl = 1;
        protected int m_Exp = 0;
        protected int m_ExpNeeded = 100;

        // change the maxmana and maxhp different for each hero
        public float m_MaxHP = 100f;
        public float m_MaxMana = 100f;

        public float m_BaseDMG;
        protected float m_CurrentHP;
        protected float m_CurrentMana;
        public float m_Armor;
        public float m_MagicResist;
        public float m_HealthRegen;
        public float m_ManaRegen;

        public Sprite[] m_MovementSprites;


        private Guid SpawnerDeathEventGuid;
        private Guid MobDeathEventGuid;

        [HideInInspector] public int bags;
        [HideInInspector] public bool overTeleport;
        [HideInInspector] public string teleportName;
        [HideInInspector] public int buffDuration;
        [HideInInspector] public int buffStrength;
        [HideInInspector] public IPotions currentBuff;


        private SpriteRenderer spriteRenderer;




        protected void Awake()
        {
            bags = 0;
            buffDuration = 0;
            buffStrength = 0;
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidbody = GetComponent<Rigidbody2D>();
            inventory = GetComponent<Inventory>();
            m_NormalizedMovement = new Vector2(1, 0);

            m_ImageCooldownBasic.fillAmount = 0;
            m_ImageCooldownSpellOne.fillAmount = 0;
            m_TextCooldownBasic.text = "";
            m_TextCooldownSpellOne.text = "";

        }

        protected void Start()
        {
            m_CurrentHP = m_MaxHP;
            m_CurrentMana = m_MaxMana;

            SetHealthUI();
            SetStats();
            SetOnClicks();
            spellOneCooldownATM = m_SpellOneCooldown;
            basicAttackCooldownATM = m_BasicAttackCooldown;

            EventSystem.Current.RegisterListener<MobDeathEventInfo>(EnemyKilled, ref MobDeathEventGuid);
            EventSystem.Current.RegisterListener<SpawnerDeathEventInfo>(EnemyKilled, ref SpawnerDeathEventGuid);

            InvokeRepeating("UpdateCd", 0.0f, 1.0f);
            InvokeRepeating("UpdateBasic", 0.0f, 0.1f);

        }

        private void UpdateBasic()
        {
            if (m_BasicAttackCooldown >= basicAttackCooldownATM) basicAttackCooldownATM+= 0.1f;
            if (buffDuration > 0)
            {
                buffDuration--;
            }
        }


        private void UpdateCd()
        {
            if (m_SpellOneCooldown > spellOneCooldownATM) spellOneCooldownATM++;
            if (spellOneCooldownATM > m_SpellOneCooldown) spellOneCooldownATM = m_SpellOneCooldown;
            // TODO the the spell cooldown will shorten 0-1 sec depending on when we press it, FIX IT
            if (m_MaxMana >= m_CurrentMana) m_CurrentMana += m_ManaRegen;
            if (m_CurrentMana > m_MaxMana) m_CurrentMana = m_MaxMana;
            if (m_MaxHP >= m_CurrentHP) m_CurrentHP += m_ManaRegen;
            if (m_CurrentHP > m_MaxHP) m_CurrentHP = m_MaxMana;

            setSpellTextOnCD(m_TextCooldownBasic, m_BasicAttackCooldown, basicAttackCooldownATM);
            setSpellTextOnCD(m_TextCooldownSpellOne, m_SpellOneCooldown, spellOneCooldownATM);

            SetHealthUI();
        }

        void Update()
        {
            //EVERY FRAME
            Quaternion q = transform.rotation;
            q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
            transform.rotation = q;

            m_ImageCooldownBasic.fillAmount = (m_BasicAttackCooldown - basicAttackCooldownATM) / m_BasicAttackCooldown;
            m_ImageCooldownSpellOne.fillAmount = (m_SpellOneCooldown - spellOneCooldownATM) / m_SpellOneCooldown;
            
            


        }

        // MOVING AND UI ---------------------------------------MOVING AND UI---------------------------------------------MOVING AND UI


        public void Move(Vector2 position)
        {

            if (buffDuration > 0)
            {
                position = position + position * buffStrength / 100.0f;
            }
            rigidbody.MovePosition(rigidbody.position + position);
            if (position.x != 0 || position.y != 0) m_NormalizedMovement = position.normalized;

            if (m_MovementSprites != null)
            {
                if (m_NormalizedMovement.y == 1.0f) spriteRenderer.sprite = m_MovementSprites[2];
                else if (m_NormalizedMovement.y == -1.0f) spriteRenderer.sprite = m_MovementSprites[0];
                else if (m_NormalizedMovement.x == 1.0f)
                {
                    spriteRenderer.sprite = m_MovementSprites[1];
                    spriteRenderer.flipX = false;
                }
                else if (m_NormalizedMovement.x == -1.0f)
                {
                    spriteRenderer.sprite = m_MovementSprites[1];
                    spriteRenderer.flipX = true;
                }
            }

        }

        protected void setSpellTextOnCD(Text cText, float cooldownMax, float cooldownATM)
        {
            if (cooldownMax <= cooldownATM) cText.text = "";
            else cText.text = ((int)(cooldownMax - cooldownATM)).ToString();
        }


        public void CollideWithBag(bool b)
        {
            if (b) bags++;
            else bags--;
            if (bags < 0) bags = 0;
        }


        protected void SetHealthUI()
        {
            m_HpSlider.value = m_CurrentHP / m_MaxHP;
            m_ManaSlider.value = m_CurrentMana / m_MaxMana;
            m_FillHPImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHP / m_MaxHP);
            m_FillManaImage.color = Color.Lerp(m_ZeroManaColor, m_FullManaColor, m_CurrentMana / m_MaxMana);
            SetStats();
        }

        protected void SetStats()
        {
            m_StatText.text = "Level: " + m_Lvl + "\nMax HP: " + m_MaxHP + "\nMax Mana: " + m_MaxMana
                            + "\nCurrent HP: " + m_CurrentHP + "\nCurrent Mana: " + m_CurrentMana
                            + "\nBase DMG: " + m_BaseDMG + "\nArmor: " + m_Armor + "\nMagic Resist: " + m_MagicResist;
        }

        private void SetOnClicks()
        {
            for (int i = 0; i < 25; i++)
            {
                int x = i;
                inventory.m_SlotHolder.transform.GetChild(i).GetComponent<MyButton>().onClick.AddListener(() => UseItem(x));
            }
            m_HelmetSlot.gameObject.GetComponent<MyButton>().onClick.AddListener(() => UnEquipItem(m_HelmetSlot));
            m_ArmorSlot.gameObject.GetComponent<MyButton>().onClick.AddListener(() => UnEquipItem(m_ArmorSlot));
            m_GlovesSlot.gameObject.GetComponent<MyButton>().onClick.AddListener(() => UnEquipItem(m_GlovesSlot));
            m_LeggingSlot.gameObject.GetComponent<MyButton>().onClick.AddListener(() => UnEquipItem(m_LeggingSlot));
            m_WeaponSlot.gameObject.GetComponent<MyButton>().onClick.AddListener(() => UnEquipItem(m_WeaponSlot));
        }


        public void TakeDamage(float amount)
        {
            m_CurrentHP -= amount;
            SetHealthUI();
            if (m_CurrentHP <= 0) Die();
        }

        private void UnEquipItem(Slot slot)
        {
            if (slot.m_item != null)
            {
                slot.m_item.m_Quantity = 1;
                Items item = slot.m_item;
                inventory.AddItem(item);
                item.UpdateStatsWithItem(this, false);
                slot.RemoveAllItemsFromSlot(false);
            }
        }

        private void UseItem(int i)
        {
            Slot currSlot = inventory.GetSlot(i);
            Items currentItem = currSlot.m_item;
            if (currentItem == null) return;

            switch (currentItem.tag)
            {
                //CHECK IF QUANTITY >1 THEN OTHER THINGS HAPPEN
                case "Helmet":
                    EquipItemByLocation(i, m_HelmetSlot);
                    break;
                case "Armor":
                    EquipItemByLocation(i, m_ArmorSlot);
                    break;
                case "Gloves":
                    EquipItemByLocation(i, m_GlovesSlot);
                    break;
                case "Legging":
                    EquipItemByLocation(i, m_LeggingSlot);
                    break;
                case "Weapon":
                    EquipItemByLocation(i, m_WeaponSlot);
                    break;
                case "SpeedPotion":
                    inventory.UseItem(this, i);
                    break;
                case "ManaPotion":
                    inventory.UseItem(this, i);
                    break;
                case "HPPotion":
                    inventory.UseItem(this, i);
                    break;
                default:
                    break;
            }
            SetStats();
        }




        private void EquipItemByLocation(int locationInInventory, Slot slotToEquiep)
        {
            if (locationInInventory != -1)
            {
                Slot CurrentSlot = inventory.m_slots[locationInInventory].GetComponent<Slot>();

                if (slotToEquiep.m_item == null)
                {
                    slotToEquiep.AddToEmptyEqupmentSlot(CurrentSlot);
                    slotToEquiep.m_item.UpdateStatsWithItem(this, true);
                }
                else
                {
                    Items item = slotToEquiep.GetComponent<Slot>().m_item;
                    slotToEquiep.RemoveItemFromSlot(false);
                    item.UpdateStatsWithItem(this, false);
                    slotToEquiep.AddToEmptyEqupmentSlot(CurrentSlot);
                    CurrentSlot.m_item.UpdateStatsWithItem(this, true);
                    item.m_Quantity = 1;
                    inventory.AddItem(item);
                }
            }
            SetStats();
        }





        private void EnemyKilled(MobDeathEventInfo mdei)
        {
            if (mdei.Killer.Equals(gameObject.name)) GetExp(mdei.Level * 15);
        }

        private void EnemyKilled(SpawnerDeathEventInfo udei)
        {
            if (udei.Killer.Equals(gameObject.name)) GetExp(udei.Level * 15);
        }


        abstract public void Attack();
        abstract public void UseSkill(int i);

        public void AddItemToInventory(Items i)
        {
            Debug.Log(i);
            inventory.AddItem(i);
        }

        abstract public void GetExp(int exp);


        public void HealHero(float heal)
        {
            m_CurrentHP += heal;
            if (m_CurrentHP > m_MaxHP)
            {
                m_CurrentHP = m_MaxHP;
            }
        }

        public void ManaHero(float mana)
        {
            m_CurrentMana += mana;
            if (m_CurrentMana > m_MaxMana)
            {
                m_CurrentMana = m_MaxMana;
            }
        }



        private void Die()
        {
            // FIRE EVENT TO GAME MANAGER, IN GAME MANAGER IF ALL THE PLAYERS ARE DEAD, THEN DO THIS
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        }
    }
}

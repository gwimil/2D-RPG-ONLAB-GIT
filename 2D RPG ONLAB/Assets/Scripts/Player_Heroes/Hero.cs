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
        public Image m_ImageCooldownSpellTwo;

        public Text m_TextCooldownBasic;
        public Text m_TextCooldownSpellOne;
        public Text m_TextCooldownSpellTwo;

        public float m_SpellOneCooldown;
        protected float spellOneCooldownATM;
        public float m_SpellOneManaCost;

        public float m_SpellTwoCooldown;
        protected float spellTwoCooldownATM;
        public float m_SpellTwoManaCost;


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


        private float timer;
        private int previousSecond;

        private string heroObjectName;
        

        private Guid SpawnerDeathEventGuid;
        private Guid MobDeathEventGuid;
        
        [HideInInspector] public int bags;
        [HideInInspector] public bool overTeleport;
        [HideInInspector] public string teleportName;




        protected void Awake()
        {
            bags = 0;
            rigidbody = GetComponent<Rigidbody2D>();
            inventory = GetComponent<Inventory>();
            heroObjectName = this.gameObject.name;
            m_NormalizedMovement = new Vector2(1, 0);

            m_ImageCooldownBasic.fillAmount = 0;
            m_ImageCooldownSpellOne.fillAmount = 0;
            m_ImageCooldownSpellTwo.fillAmount = 0;
            m_TextCooldownBasic.text = "";
            m_TextCooldownSpellOne.text = "";
            m_TextCooldownSpellTwo.text = "";

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

            previousSecond = 0;
            timer = 0.0f;

            EventSystem.Current.RegisterListener<MobDeathEventInfo>(EnemyKilled, ref MobDeathEventGuid);
            EventSystem.Current.RegisterListener<SpawnerDeathEventInfo>(EnemyKilled, ref SpawnerDeathEventGuid);
        }

        void Update()
        {
            //EVERY FRAME
            Quaternion q = transform.rotation;
            q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
            transform.rotation = q;

            m_ImageCooldownBasic.fillAmount = (m_BasicAttackCooldown - basicAttackCooldownATM) / m_BasicAttackCooldown;
            m_ImageCooldownSpellOne.fillAmount = (m_SpellOneCooldown - spellOneCooldownATM) / m_SpellOneCooldown;
            if (m_BasicAttackCooldown >= basicAttackCooldownATM) basicAttackCooldownATM++;

            timer += Time.deltaTime;
            int second = Convert.ToInt32(timer % 5000);
            // EVERY SECOND
            if (second - previousSecond == 1)
            {
                previousSecond++;

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
                if (second == 5000)
                {
                    timer = 0.0f;
                    previousSecond = 0;
                }
            }
        }

        // MOVING AND UI ---------------------------------------MOVING AND UI---------------------------------------------MOVING AND UI


        public void Move(Vector2 position)
        {
            rigidbody.MovePosition(rigidbody.position + position);
            if (position.x != 0 || position.y != 0) m_NormalizedMovement = position.normalized;
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
                    // TODO
                    break;
                case "ManaPotion":
                    // TODO
                    break;
                case "HPPotion":
                    // TODO
                    break;
                case "DefensePotion":
                    // TODO
                    break;
                default:
                    break;
            }
            SetStats();
        }
        

        

        public void EquipBestItems()
        {
            EquipItemByLocation(inventory.GetBestItemByTag("Weapon"), m_WeaponSlot);
            EquipItemByLocation(inventory.GetBestItemByTag("Helmet"), m_HelmetSlot);
            EquipItemByLocation(inventory.GetBestItemByTag("Gloves"), m_GlovesSlot);
            EquipItemByLocation(inventory.GetBestItemByTag("Legging"), m_LeggingSlot);
            EquipItemByLocation(inventory.GetBestItemByTag("Armor"), m_ArmorSlot);
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



        private void Die()
        {
            // FIRE EVENT TO GAME MANAGER, IN GAME MANAGER IF ALL THE PLAYERS ARE DEAD, THEN DO THIS
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        }
    }
}

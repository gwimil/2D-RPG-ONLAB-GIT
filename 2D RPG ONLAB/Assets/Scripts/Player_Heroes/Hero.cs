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
        protected Inventory inventory;

        public int m_Lvl = 1;
        protected int m_Exp = 0;
        protected int m_ExpNeeded = 100;

        // change the maxmana and maxhp different for each hero
        protected float m_MaxHP = 100f;
        protected float m_MaxMana = 100f;

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

        private Boolean overBag;
        private Bag bag;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            inventory = GetComponent<Inventory>();

            heroObjectName = this.gameObject.name;

            EventSystem.Current.RegisterListener<UnitDeathEventInfo>(EnemyKilled);

            m_CurrentHP = m_MaxHP;
            m_CurrentMana = m_MaxMana;

            SetHealthUI();
            SetStats();
            SetOnClicks();

            m_NormalizedMovement = new Vector2(1, 0);

            spellOneCooldownATM = m_SpellOneCooldown;
            basicAttackCooldownATM = m_BasicAttackCooldown;

            m_ImageCooldownBasic.fillAmount = 0;
            m_ImageCooldownSpellOne.fillAmount = 0;
            m_ImageCooldownSpellTwo.fillAmount = 0;
            m_TextCooldownBasic.text = "";
            m_TextCooldownSpellOne.text = "";
            m_TextCooldownSpellTwo.text = "";

            overBag = false;

            previousSecond = 0;
            timer = 0.0f;
        }

        void Update()
        {
            //EVERY FRAME
            Quaternion q = transform.rotation;
            q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
            transform.rotation = q;

            if (inventory.m_inventoryEnabled)
                if (Input.GetKeyDown(KeyCode.C))
                {
                    EquipBestItems();
                }

            if (overBag)
            {
                if (bag != null && bag.gameObject != null)
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        for (int i = 0; i < bag.items.Count; i++)
                        {
                            inventory.AddItem(bag.items[i]);
                        }
                        overBag = false;
                        Destroy(bag.gameObject);
                        bag = null;

                    }
            }




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

        protected void setSpellTextOnCD(Text cText, float cooldownMax, float cooldownATM)
        {
            if (cooldownMax <= basicAttackCooldownATM) cText.text = "";
            else cText.text = ((int)(cooldownMax - cooldownATM)).ToString();
        }


        public void Move(Vector2 position)
        {
            rigidbody.MovePosition(rigidbody.position + position);
            if (position.x != 0 || position.y != 0) m_NormalizedMovement = position.normalized;
        }

        public void TakeDamage(float amount)
        {
            m_CurrentHP -= amount;
            SetHealthUI();
            if (m_CurrentHP <= 0) Die();
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
                inventory.m_SlotHolder.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(() => UseItem(x));
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
                    ChangeItemsInInventory(m_HelmetSlot, currSlot);
                    break;
                case "Armor":
                    ChangeItemsInInventory(m_ArmorSlot, currSlot);
                    break;
                case "Gloves":
                    ChangeItemsInInventory(m_GlovesSlot, currSlot);
                    break;
                case "Legging":
                    ChangeItemsInInventory(m_LeggingSlot, currSlot);
                    break;
                case "Weapon":
                    ChangeItemsInInventory(m_WeaponSlot, currSlot);
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
            UpdateStats();
        }

        private void ChangeItemsInInventory(Slot profSlot, Slot currSlot)
        {
            if (profSlot.m_item == null)
            {
                profSlot.AddToEmptyEqupmentSlot(currSlot);
            }
            else
            {
                Items item = profSlot.GetComponent<Slot>().m_item;
                profSlot.RemoveItemFromSlot(false);
                profSlot.AddToEmptyEqupmentSlot(currSlot);
                item.m_Quantity = 1;
                inventory.AddItem(item);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "ItemHolder")
            {
                overBag = true;
                bag = collision.GetComponent<Bag>();
                bag.image.gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "ItemHolder")
            {
                if (overBag && bag != null)
                {
                    overBag = false;
                    bag.image.gameObject.SetActive(false);
                    bag = null;
                }

            }
        }

        public void EquipBestItems()
        {
            int locationInInventory = inventory.GetBestItemByTag("Weapon");
            if (locationInInventory != -1)
            {
                Slot CurrentSlot = inventory.m_slots[locationInInventory].GetComponent<Slot>();


                if (m_WeaponSlot.m_item == null)
                {
                    m_WeaponSlot.AddToEmptyEqupmentSlot(CurrentSlot);
                }
                else
                {
                    Items item = m_WeaponSlot.GetComponent<Slot>().m_item;
                    m_WeaponSlot.RemoveItemFromSlot(false);
                    m_WeaponSlot.AddToEmptyEqupmentSlot(CurrentSlot);
                    item.m_Quantity = 1;
                    inventory.AddItem(item);
                }

            }

            // TODO


        }



        private void UpdateStats()
        {
            //if (m_ArmorSlot.m_item != null) ; TODO change stats 
        }

        private void EnemyKilled(UnitDeathEventInfo udei)
        {
            int enemLevel = udei.Level;
            GetExp(enemLevel * 15);
        }


        abstract public void Attack();
        abstract public void UseSkill(int i);
        abstract public void AddItemToInventory(Items i);
        abstract public void GetExp(int exp);

        private void Die()
        {
            // FIRE EVENT TO GAME MANAGER, IN GAME MANAGER IF ALL THE PLAYERS ARE DEAD, THEN DO THIS
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        }





    }
}

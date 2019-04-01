using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Hero : MonoBehaviour
{

    public Boolean SpellOneOnCooldown;

    protected Vector2 m_NormalizedMovement;

    public Slider m_HpSlider;
    public Slider m_ManaSlider;
    public Color m_FullHealthColor = Color.green;
    public Color m_ZeroHealthColor = Color.red;
    public Color m_FullManaColor = Color.blue;
    public Color m_ZeroManaColor = Color.grey;
    public Image m_FillHPImage;
    public Image m_FillManaImage;


    public Slot m_HelmetSlot;
    public Slot m_ArmorSlot;
    public Slot m_GlovesSlot;
    public Slot m_LeggingSlot;
    public Slot m_WeaponSlot;


    public Text m_StatText;

    protected Rigidbody2D rigidbody;
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


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        inventory = GetComponent<Inventory>();
        m_CurrentHP = m_MaxHP;
        m_CurrentMana = m_MaxMana;
        SetHealthUI();
        SetStats();
        SetOnClicks();
        m_NormalizedMovement = new Vector2(1, 0);
    }

    void Update()
    {
        Quaternion q = transform.rotation;
        q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
        transform.rotation = q;
    }
    

    public void Move(Vector2 position)
    {
        rigidbody.MovePosition(rigidbody.position + position);
        if (position.x!=0||position.y!=0) m_NormalizedMovement = position.normalized;
    }

    public void TakeDamage(float amount)
    {
        m_CurrentHP -= amount;
        SetHealthUI();
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
            inventory.m_SlotHolder.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(delegate { UseItem(i); });

     }

    private void UseItem(int i)
    {
        Slot currSlot = inventory.m_SlotHolder.transform.GetChild(i).GetComponent<Slot>();
        Items currentItem = currSlot.m_item;
        if (currentItem == null) return ;

        switch (currentItem.tag)
            {
                //CHECK IF QUANTITY >1 THEN OTHER THINGS HAPPEN
                case "Helmet":
                ChangeItemsInInventory(m_HelmetSlot, currSlot, currentItem);
                    break;
                case "Armor":
                ChangeItemsInInventory(m_ArmorSlot, currSlot, currentItem);
                break;
                case "Gloves":
                ChangeItemsInInventory(m_GlovesSlot, currSlot, currentItem);
                break;
                case "Legging":
                ChangeItemsInInventory(m_LeggingSlot, currSlot, currentItem);
                break;
                case "Weapon":
                ChangeItemsInInventory(m_WeaponSlot, currSlot, currentItem);
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

    private void ChangeItemsInInventory(Slot profSlot, Slot currSlot ,Items currItem)
    {
        if (profSlot.m_item == null)
        {
            m_HelmetSlot.AddItemToSlot(currItem);
            currSlot.RemoveItemFromSlot();
        }
        else
        {
            Items temp = profSlot.m_item;
            profSlot.RemoveItemFromSlot();
            profSlot.AddItemToSlot(currItem);
            currSlot.RemoveItemFromSlot();
            currSlot.AddItemToSlot(temp);
        }
    }

    private void UpdateStats()
    {
        if (m_ArmorSlot.m_item != null) ;// change stats 
    }
    

    abstract public void Attack();
    abstract public void UseSkill(int i);
    abstract public void AddItemToInventory(Items i);
    abstract public void GetExp(int exp);


}


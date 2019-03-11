using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Hero : MonoBehaviour
{


    public Slider m_HpSlider;
    public Slider m_ManaSlider;
    public Color m_FullHealthColor = Color.green;
    public Color m_ZeroHealthColor = Color.red;
    public Color m_FullManaColor = Color.blue;
    public Color m_ZeroManaColor = Color.grey;
    public Image m_FillHPImage;
    public Image m_FillManaImage;

    protected Rigidbody2D rigidbody;
    protected Inventory inventory;

    public int m_Lvl = 1;
    protected int m_Exp = 0;
    protected int m_ExpNeeded = 100;

    // change the maxmana and maxhp different for each hero
    protected float m_MaxHP = 100f;
    protected float m_MaxMana = 100f;

    public float m_BaseDMG;
    public float m_CurrentHP;
    public float m_CurrentMana;
    public float m_Armor;
    public float m_MagicResist;

    abstract public void TakeDamage(float f);
    abstract public void GetExp(int exp);
    abstract public void Move(Vector2 vector);
    abstract public void Attack();
    abstract public void UseSkill(int i);
    abstract public void AddItemToInventory(Items i);
}

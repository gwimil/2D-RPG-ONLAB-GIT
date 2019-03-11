using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Hero
{
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        inventory = GetComponent<Inventory>();
        m_CurrentHP = m_MaxHP;
        m_CurrentMana = m_MaxMana;
        SetHealthUI();
    }

    void Update()
    {
        Quaternion q = transform.rotation;
        q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
        transform.rotation = q;
    }
    

    override public void Move(Vector2 position)
    {
        rigidbody.MovePosition(rigidbody.position + position);
    }

    override public void Attack()
    {

    }
    override public void UseSkill(int i)
    {
        // do it with enum / switch case
    }
    override public void AddItemToInventory(Items i)
    {
        inventory.AddItem(i);
    }

    override public void TakeDamage(float amount)
    {
        m_CurrentHP -= amount;
        SetHealthUI();
    }

    private void SetHealthUI()
    {
        
        m_HpSlider.value = m_CurrentHP / m_MaxHP;
        m_ManaSlider.value = m_CurrentMana/ m_MaxMana;

       
        m_FillHPImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHP / m_MaxHP);
        m_FillManaImage.color = Color.Lerp(m_ZeroManaColor, m_FullManaColor, m_CurrentMana / m_MaxMana);
    }


    public override void GetExp(int exp)
    {
        m_Exp += exp;
        if (m_Exp > m_ExpNeeded)
        {
            m_Exp -= m_ExpNeeded;
            m_ExpNeeded += 100;
            m_Lvl++;

            //theser should be different fro each hiro !
            m_MaxHP += 50;
            m_MaxMana += 50;
            m_CurrentHP = m_MaxHP;
            m_CurrentMana = m_MaxMana;
            m_Armor += 5;
            m_MagicResist += 10;
            m_BaseDMG += 15;

            // unlock thing if LVL is high enough with a function
            // private void LevelUp();

            //play animation for level up

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Hero
{
  
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

            // MANA REGEN / HP REGEN

            // unlock thing if LVL is high enough with a function
            // private void LevelUp();

            //play animation for level up

        }
    }

}

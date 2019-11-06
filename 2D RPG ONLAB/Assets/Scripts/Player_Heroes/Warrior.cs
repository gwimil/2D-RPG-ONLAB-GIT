using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace EventCallbacks
{
    public class Warrior : Hero
    {
        public GameObject m_ShieldBot;
        public GameObject m_AttackBot;

        override public void Attack()
        {
            if (m_BasicAttackCooldown <= basicAttackCooldownATM && m_ShieldBot != null)
            {
                GameObject p = Instantiate(m_ShieldBot, transform.position, Quaternion.Euler(0, 0, 0));
                p.SetActive(true);
                basicAttackCooldownATM = 0.0f;
                m_TextCooldownBasic.text = ((int)m_BasicAttackCooldown).ToString();
            }
        }

        override public void UseSkill(int i)
        {
            if (i == 1)
            {
                if (m_SpellOneCooldown == spellOneCooldownATM && m_CurrentMana >= m_SpellOneManaCost && m_AttackBot != null)
                {
                    GameObject p = Instantiate(m_AttackBot, transform.position, Quaternion.Euler(0, 0, 0));
                    p.SetActive(true);
                    // p.m_damage += m_BaseDMG / 10;
                    m_CurrentMana -= m_SpellOneManaCost;
                    spellOneCooldownATM = 0.0f;
                    m_TextCooldownSpellOne.text = ((int)m_SpellOneCooldown).ToString();
                }
            }
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
                m_MaxHP += 100;
                m_MaxMana += 50;
                m_CurrentHP = m_MaxHP;
                m_CurrentMana = m_MaxMana;
                m_Armor += 10;
                m_MagicResist += 10;
                m_BaseDMG += 10;

                // MANA REGEN / HP REGEN

                // unlock thing if LVL is high enough with a function
                // private void LevelUp();

                //play animation for level up

            }
        }
    }
}
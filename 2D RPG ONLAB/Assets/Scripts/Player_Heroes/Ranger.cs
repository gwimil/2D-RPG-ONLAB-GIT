using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public class Ranger : Hero
    {

        public Projectile m_SpellOne;
        public Projectile m_BaseAttack;

        override public void Attack()
        {
            if (m_BasicAttackCooldown <= basicAttackCooldownATM)
            {
                Projectile p = Instantiate(m_BaseAttack, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                p.setDirection(m_NormalizedMovement);
                p.m_damage += m_BaseDMG / 15;
                basicAttackCooldownATM = 0.0f;
            }
        }
        override public void UseSkill(int i)
        {
            switch (i)
            {
                case 1:
                    if (m_SpellOneCooldown == spellOneCooldownATM && m_CurrentMana >= m_SpellOneManaCost && m_SpellOne != null)
                    {
                        Projectile fb = Instantiate(m_SpellOne, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
                        fb.setDirection(m_NormalizedMovement);
                        fb.m_damage += m_BaseDMG / 10;
                        fb.user = gameObject.name;
                        m_CurrentMana -= m_SpellOneManaCost;
                        spellOneCooldownATM = 0.0f;
                    }
                    break;
                case 2:
                    break;

                default: break;
            }

            SetHealthUI();
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
                m_Armor += 10;
                m_MagicResist += 5;
                m_BaseDMG += 15;

                // MANA REGEN / HP REGEN

                // unlock thing if LVL is high enough with a function
                // private void LevelUp();

                //play animation for level up

            }
        }

    }
}


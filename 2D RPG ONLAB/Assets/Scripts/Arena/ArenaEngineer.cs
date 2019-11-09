﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace EventCallbacks
{
    public class ArenaEngineer : ArenaHeroes
    {

        public GameObject m_ShieldBot;
        public GameObject m_AttackBot;




        public override void Attack()
        {
            if (!hasAuthority)
            {
                return;
            }

            if (m_BasicAttackCooldown <= basicAttackCooldownATM && m_AttackBot != null)
            {
                CmdSpawnMyAttack();
                basicAttackCooldownATM = 0.0f;
            }
        }

        public override void UseSkills(int i)
        {
            if (!hasAuthority)
            {
                return;
            }

            switch (i)
            {
                case 1:
                    if (m_SpellOneCooldown == spellOneCooldownATM && m_ShieldBot != null)
                    {
                        
                    //    CmdSpawnHealBot();
                        spellOneCooldownATM = 0.0f;
                        m_TextCooldownSpellOne.text = ((int)m_SpellOneCooldown).ToString();
                    }
                    break;
                case 2:
                    break;

                default: break;
            }
        }

        [Command]
        void CmdSpawnMyAttack()
        {
            GameObject p = Instantiate(m_AttackBot, transform.position, Quaternion.Euler(0, 0, 0));
            p.GetComponent<ArenaFireClosestEnemyBot>().ID = ID;

            NetworkServer.Spawn(p);
        }


      /*  [Command]
        void CmdSpawnHealBot()
        {
            GameObject p = Instantiate(m_ShieldBot, transform.position, Quaternion.Euler(0, 0, 0));
            p.GetComponent<ArenaHealBot>().ID = ID;

            NetworkServer.Spawn(p);
        }*/




    }

}

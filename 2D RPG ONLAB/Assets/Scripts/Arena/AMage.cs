using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace EventCallbacks
{
    public class AMage : ArenaHeroes
    {

        public ArenaProjectiles m_FireBall;
        public ArenaProjectiles m_BaseAttack;

        override public void Attack()
        {

            if (!hasAuthority)
            {
                return;
            }

            if (m_BasicAttackCooldown <= basicAttackCooldownATM)
            {
                CmdSpawnMyProjectile();
                basicAttackCooldownATM = 0.0f;
                m_TextCooldownBasic.text = ((int)m_BasicAttackCooldown).ToString();
            }
        }

        override public void UseSkills(int i)
        {
            if (!hasAuthority)
            {
                return;
            }

            switch (i)
            {
                case 1:
                    if (m_SpellOneCooldown == spellOneCooldownATM && m_CurrentMana >= m_SpellOneManaCost && m_FireBall != null)
                    {
                        CmdSpawnMyFireProjectile();
                        m_CurrentMana -= m_SpellOneManaCost;
                        spellOneCooldownATM = 0.0f;
                        m_TextCooldownSpellOne.text = ((int)m_SpellOneCooldown).ToString();
                    }
                    break;
                case 2:
                    break;

                default: break;
            }

            SetHealthUI();
        }

        [Command]
        void CmdSpawnMyProjectile()
        {

            // We are guaranteed to be on the server right now.
            ArenaProjectiles p = Instantiate(m_BaseAttack, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
            p.setDirection(m_NormalizedMovement);
            p.m_damage += m_BaseDMG / 15;

            //go.GetComponent<NetworkIdentity>().AssignClientAuthority( connectionToClient );

            // Now that the object exists on the server, propagate it to all
            // the clients (and also wire up the NetworkIdentity)
            NetworkServer.SpawnWithClientAuthority(p.gameObject, connectionToClient);
        }

        [Command]
        void CmdSpawnMyFireProjectile()
        {

            // We are guaranteed to be on the server right now.
            ArenaProjectiles fb = Instantiate(m_FireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
            fb.setDirection(m_NormalizedMovement);
            fb.m_damage += m_BaseDMG / 10;
            fb.user = gameObject.name;

            //go.GetComponent<NetworkIdentity>().AssignClientAuthority( connectionToClient );

            // Now that the object exists on the server, propagate it to all
            // the clients (and also wire up the NetworkIdentity)
            NetworkServer.SpawnWithClientAuthority(fb.gameObject, connectionToClient);
        }

    }
}
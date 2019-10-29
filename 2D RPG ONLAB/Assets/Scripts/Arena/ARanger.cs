using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace EventCallbacks
{
    public class ARanger : ArenaHeroes
    {

        public ArenaProjectiles m_SpellOne;
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
                    if (m_SpellOneCooldown == spellOneCooldownATM && m_SpellOne != null)
                    {
                        List<ArenaProjectiles> multipleArrows = new List<ArenaProjectiles>();
                        for (int j = -2; j < 3; j++)
                        {
                            multipleArrows.Add(Instantiate(m_SpellOne, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180)));
                            if (m_NormalizedMovement.y == 0.0f)
                            {
                          //      multipleArrows[j + 2].setDirection(m_NormalizedMovement + new Vector2(0.0f, 0.2f * j));
                            }
                            else if (m_NormalizedMovement.x == 0.0f)
                            {
                            //    multipleArrows[j + 2].setDirection(m_NormalizedMovement + new Vector2(0.2f * j, 0.0f));
                            }
                            else if (m_NormalizedMovement.x == m_NormalizedMovement.y)
                            {
                           //     multipleArrows[j + 2].setDirection(m_NormalizedMovement + new Vector2(0.14f * j, -0.14f * j));
                            }
                            else
                            {
                           //     multipleArrows[j + 2].setDirection(m_NormalizedMovement + new Vector2(0.14f * j, 0.14f * j));
                            }

                            multipleArrows[j + 2].m_damage += m_BaseDMG / 10;
                        }
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
        void CmdSpawnMyProjectile()
        {

            // We are guaranteed to be on the server right now.
            ArenaProjectiles p = Instantiate(m_BaseAttack, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
         //   p.setDirection(m_NormalizedMovement);
            p.m_damage += m_BaseDMG / 15;

            //go.GetComponent<NetworkIdentity>().AssignClientAuthority( connectionToClient );

            // Now that the object exists on the server, propagate it to all
            // the clients (and also wire up the NetworkIdentity)
            NetworkServer.SpawnWithClientAuthority(p.gameObject, connectionToClient);
        }

    }
}


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
                CmdFire();
                basicAttackCooldownATM = 0.0f;
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
                    if (m_SpellOneCooldown == spellOneCooldownATM  && m_FireBall != null)
                    {
                        CmdSpawnMyFireProjectile();
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
        void CmdFire()
        {

            // We are guaranteed to be on the server right now.
            
            GameObject p = Instantiate(m_BaseAttack.gameObject, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
            //  p.setDirection(m_NormalizedMovement)
            p.GetComponent<ArenaProjectiles>().ID = ID;
            Debug.Log(GetComponent<NetworkTransform>().transform.rotation);
            p.GetComponent<Rigidbody2D>().velocity = m_NormalizedMovement * 5;
            p.GetComponent<ArenaProjectiles>().m_damage += m_BaseDMG / 10;
            p.GetComponent<ArenaProjectiles>().direction = Quaternion.Euler(0, 0, transform.rotation.z + 180);
            //go.GetComponent<NetworkIdentity>().AssignClientAuthority( connectionToClient );

            // Now that the object exists on the server, propagate it to all
            // the clients (and also wire up the NetworkIdentity)
            NetworkServer.Spawn(p);
            

            Destroy(p, 3);
        }





        [Command]
        void CmdSpawnMyFireProjectile()
        {

            // We are guaranteed to be on the server right now.
            GameObject fb = Instantiate(m_FireBall.gameObject, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
            fb.GetComponent<ArenaProjectiles>().ID = ID;
            fb.gameObject.GetComponent<Rigidbody2D>().velocity = m_NormalizedMovement * 5;
            fb.GetComponent<ArenaProjectiles>().m_damage += m_BaseDMG / 10;
            fb.GetComponent<ArenaProjectiles>().direction = Quaternion.Euler(0, 0, transform.rotation.z + 90);

            //go.GetComponent<NetworkIdentity>().AssignClientAuthority( connectionToClient );

            // Now that the object exists on the server, propagate it to all
            // the clients (and also wire up the NetworkIdentity)
            NetworkServer.Spawn(fb);
            
            Destroy(fb, 3);

        }


        
    }
}
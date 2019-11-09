using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace EventCallbacks
{
    public class ArenaFireClosestEnemyBot : NetworkBehaviour
    {

        public ArenaProjectiles m_ProjectileToFire;

        [HideInInspector]public int ID;


        void Start()
        {
            InvokeRepeating("ShootClosestEnemy", 1.0f, 1.3f);
        }


        private void ShootClosestEnemy()
        {
            GameObject[] playersToFind = GameObject.FindGameObjectsWithTag("Hero");
            GameObject closestEnemy = null;
            float closestDistance = 7;
            Vector2 closestNormalizedMovement = new Vector2(0, 0);
            for (int i = 0; i < playersToFind.Length; i++)
            {
                if (playersToFind[i].gameObject.GetComponent<ArenaHeroes>().ID != ID)
                {
                    Vector2 playerPos = playersToFind[i].transform.position;
                    Vector2 currPos = this.transform.position;
                    float distance = Vector2.Distance(currPos, playerPos);
                    Vector2 normalizedMovement = (playerPos - currPos).normalized;

                    if (distance < 50)
                    {
                        if (closestDistance > distance)
                        {
                            closestEnemy = playersToFind[i];
                            closestDistance = distance;
                            closestNormalizedMovement = normalizedMovement;
                        }
                    }
                }
            }

            if (closestEnemy != null)
            {
                CmdFireProjectile(closestNormalizedMovement);
            }
        }


        [Command]
        void CmdFireProjectile(Vector3 normalizedMovement)
        {
            GameObject fb = Instantiate(m_ProjectileToFire.gameObject, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
            fb.GetComponent<Rigidbody2D>().velocity = normalizedMovement * 3.5f;
            fb.GetComponent<ArenaProjectiles>().ID = ID;

            NetworkServer.Spawn(fb);
        }


    }

}

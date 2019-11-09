using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace EventCallbacks
{
    public abstract class ArenaProjectiles : NetworkBehaviour
    {

        [SyncVar]
        [HideInInspector]public int ID;

        public int m_damage;
        private Transform parentTransform;
        public float SpeedInUnitsPerSecond;

        [ClientRpc]
        public void RpcChangeRotation(Vector3 dir)
        {
            transform.Rotate(0, 0, transform.rotation.z);
            float angle = Vector2.SignedAngle(dir, new Vector2(1, 0));
            transform.Rotate(0, 0, -angle);
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Hero")
            {
                ArenaHeroes otherHero = other.gameObject.GetComponent<ArenaHeroes>();
                if (ID != otherHero.ID)
                {
                    otherHero.TakeDamage(m_damage);
                    NetworkServer.Destroy(this.gameObject);
                }
            }
            else if (other.tag == "Bot")
            {
                if (other.gameObject.GetComponent<ArenaShieldBotID>().ID != ID)
                {
                    NetworkServer.Destroy(this.gameObject);
                }
            }
            else
            {
                NetworkServer.Destroy(this.gameObject);
            }
        }


    }
}
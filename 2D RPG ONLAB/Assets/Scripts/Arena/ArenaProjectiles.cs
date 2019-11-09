using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace EventCallbacks
{
    public abstract class ArenaProjectiles : NetworkBehaviour
    {

        [SyncVar(hook = "ChangeRotation")]
        [HideInInspector] public Quaternion direction;

        [SyncVar]
        [HideInInspector]public int ID;

        public int m_damage;
        private Transform parentTransform;
        public float SpeedInUnitsPerSecond;

        private void Start()
        {

        }

        void ChangeRotation(Quaternion dir)
        {
            transform.rotation = dir;
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
            else
            {
                NetworkServer.Destroy(this.gameObject);
            }
        }


    }
}
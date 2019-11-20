using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EventCallbacks
{
    public class RainProjectiles : MonoBehaviour
    {

        public Projectile m_EnemyFireBall;

        public bool rain;

        public Vector2 m_FirstProjectilePosition;
        public float m_ProjectileDistance;
        public int m_ProjectileNumber;
        public int m_ProjectileLifeTime;
        public float m_SpawnTime;
        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("LaunchProjectiles", 2.0f, m_SpawnTime);
        }

        public void ResetInvoke()
        {
            CancelInvoke();
            InvokeRepeating("LaunchProjectiles", 2.0f, m_SpawnTime);
        }

        private void LaunchProjectiles()
        {
            if (rain)
            {
                int randomCounter = Random.Range(0, m_ProjectileNumber);
                Projectile fb1 = Instantiate(m_EnemyFireBall, m_FirstProjectilePosition + new Vector2(m_ProjectileDistance * randomCounter, 0), Quaternion.Euler(0, 0, transform.rotation.z + 90));
                fb1.setDirection(new Vector2(0, -1));
            }
            
        }


    }
}


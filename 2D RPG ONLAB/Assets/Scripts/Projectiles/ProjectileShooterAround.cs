using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EventCallbacks
{
    public class ProjectileShooterAround : MonoBehaviour
    {

        public bool shootingEnabled;

        public float m_SpawnRate;
        public float m_StartAfter;
        public Projectile m_ProjectileToShoot;

        public bool isRandomRate;

        public bool m_ShootLeft;
        public bool m_ShootRight;
        public bool m_ShootTop;
        public bool m_ShootBot;
        public bool m_ShootTopLeft;
        public bool m_ShootBotLeft;
        public bool m_ShootTopRight;
        public bool m_ShootBotRight;



        // Start is called before the first frame update
        void Start()
        {
            if (isRandomRate)
            {
                float newRandomRate = Random.Range(1.5f, 2.5f);
                InvokeRepeating("Shoot", m_StartAfter, newRandomRate);
            }
            else
            {
                InvokeRepeating("Shoot", m_StartAfter, m_SpawnRate);
            }
            
        }

        private void Shoot()
        {

            if (shootingEnabled)
            {
                if (m_ShootRight)
                {
                    Projectile fb1 = Instantiate(m_ProjectileToShoot, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb1.setDirection(new Vector2(1, 0));
                }
                if (m_ShootLeft)
                {
                    Projectile fb3 = Instantiate(m_ProjectileToShoot, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb3.setDirection(new Vector2(-1, 0));
                }
                if (m_ShootTop)
                {
                    Projectile fb2 = Instantiate(m_ProjectileToShoot, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb2.setDirection(new Vector2(0, 1));
                }
                if (m_ShootBot)
                {
                    Projectile fb4 = Instantiate(m_ProjectileToShoot, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb4.setDirection(new Vector2(0, -1));
                }
                if (m_ShootTopLeft)
                {
                    Projectile fb6 = Instantiate(m_ProjectileToShoot, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb6.setDirection(new Vector2(-0.7f, 0.7f));
                }
                if (m_ShootBotLeft)
                {
                    Projectile fb8 = Instantiate(m_ProjectileToShoot, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb8.setDirection(new Vector2(-0.7f, -0.7f));
                }
                if (m_ShootTopRight)
                {
                    Projectile fb5 = Instantiate(m_ProjectileToShoot, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb5.setDirection(new Vector2(0.7f, 0.7f));
                }
                if (m_ShootBotRight)
                {
                    Projectile fb7 = Instantiate(m_ProjectileToShoot, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb7.setDirection(new Vector2(0.7f, -0.7f));
                }
            }
           
        }

        public void ResetShootRate(float newStart, float newRate)
        {
            CancelInvoke();
            InvokeRepeating("Shoot", newStart, newRate);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EventCallbacks
{
    public class MageBoss : Mobs
    {
       

        public Projectile m_EnemyFireBall;

        private bool inMiddle;
        private bool increase;
        private int counter;
        public float m_CenterX;
        public float m_CenterY;


        public override void Attack(Vector2 Dir)
        {
            
        }

        public override void ManageMovement()
        {
           
        }

        protected override void Movement(Vector2 Dir)
        {
            
        }

        // Start is called before the first frame update
        void Start()
        {
            inMiddle = true;
            increase = true;
            InvokeRepeating("LaunchProjectile", 2.0f, 0.3f);
        }

        void LaunchProjectile()
        {
            if (inMiddle)
            {
                
                Projectile fb1 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
                fb1.setDirection(new Vector2(1, 0));

                Projectile fb2 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
                fb2.setDirection(new Vector2(0, 1));

                Projectile fb3 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
                fb3.setDirection(new Vector2(-1, 0));

                Projectile fb4 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
                fb4.setDirection(new Vector2(0, -1));

                if (increase)
                {

                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}

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

        public float m_JumpRadius;
        public Vector2 m_Middle;

        private Fireball4Ways fireballMaker;

        private int phaseCounter;
        private int jumpCounter;
        private int fireBallCounter;

        private void Awake()
        {
            this.fireballMaker = gameObject.GetComponent<Fireball4Ways>();
            
            phaseCounter = 1;
            fireBallCounter = 0;
            jumpCounter = 0;
        }


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
            fireballMaker.startAttacking(false);
            InvokeRepeating("StartPlaying", 2.0f, 0.5f);
        }

        private void StartPlaying(){

            switch (phaseCounter)
            {
                case 1:
                    fireballMaker.startAttacking(true);
                    fireBallCounter++;
                    if (fireBallCounter >= 30)
                    {
                        fireballMaker.rotateDirection = fireballMaker.rotateDirection * -1;
                        fireBallCounter = 0;
                    }
                    break;
                case 2:
                    bool nearplayer = true;
                    float randomX = gameObject.transform.position.x;
                    float randomY = gameObject.transform.position.y;
                    GameObject[] playersToFind = GameObject.FindGameObjectsWithTag("Hero");

                    while (nearplayer)
                    {
                        randomX = Random.Range(m_Middle.x - m_JumpRadius / 2, m_Middle.x + m_JumpRadius / 2);
                        randomY = Random.Range(m_Middle.y - m_JumpRadius / 2, m_Middle.y + m_JumpRadius / 2);
                        bool atLeastOnePlayerNear = false;
                        for (int i = 0; i < playersToFind.Length; i++)
                        {
                            Vector2 playerPos = playersToFind[i].transform.position;
                            Vector2 currPos = this.transform.position;
                            float distance = Vector2.Distance(new Vector2(randomX,randomY), playerPos);
                            if (distance < 2)
                            {
                                atLeastOnePlayerNear = true;
                            }
                            nearplayer = atLeastOnePlayerNear;
                        }
                    }

                    gameObject.transform.position = new Vector3(randomX, randomY, 0);

                    Projectile fb1 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
                    fb1.setDirection(new Vector2(1,0));

                    Projectile fb2 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
                    fb2.setDirection(new Vector2(0, 1));

                    Projectile fb3 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
                    fb3.setDirection(new Vector2(-1, 0));

                    Projectile fb4 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
                    fb4.setDirection(new Vector2(0, -1));

                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }



        }


        private void Update()
        {
            if (m_MaxHP * 1/5 > m_HP)
            {
                phaseCounter = 5;
            }
            else if (m_MaxHP * 2 / 5 > m_HP)
            {
                phaseCounter = 4;
            }
            else if (m_MaxHP * 3 / 5 > m_HP)
            {
                phaseCounter = 3;
            }
            else if (m_MaxHP * 4 / 5 > m_HP)
            {
                phaseCounter = 2;
            }
        }

    }

}

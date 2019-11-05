using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EventCallbacks
{
    public class MageBoss : Mobs
    {

        public Projectile m_EnemyFireBall;
        public GameObject m_RainFireBall;
        public GameObject m_Teleporter;

        private bool inMiddle;
        private bool increase;
        private int counter;


        public RectTransform healthBar;

        public float m_JumpRadius;
        public Vector2 m_Middle;

        private Fireball4Ways fireballMaker;

        private int phaseCounter;
        private int jumpCounter;
        private int fireBallCounter;

        private bool started;

        private void Awake()
        {
            this.fireballMaker = gameObject.GetComponent<Fireball4Ways>();
            renderer = GetComponentInChildren<SpriteRenderer>();
            originalColor = renderer.color;
            rigidbody = GetComponent<Rigidbody2D>();
            started = false;
            phaseCounter = 1;
            fireBallCounter = 0;
            jumpCounter = 0;

            m_RainFireBall = GameObject.FindGameObjectWithTag("RainFireBall");

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

        public override void TakeDamage(float dmg, string killer)
        {
            this.killer = killer;
            m_HP -= dmg;
            renderer.color = Color.red;
            healthBar.sizeDelta = new Vector2(m_HP/50 * 3, healthBar.sizeDelta.y);
            Invoke("ResetColor", 0.2f);
            CheckIfDeath();

        }


        // Start is called before the first frame update
        void Start()
        {
            inMiddle = true;
            increase = true;
            fireballMaker.startAttacking(false);
        }

        private void StartPlaying(){

            switch (phaseCounter)
            {
                case 1:
                    fireballMaker.startAttacking(true);
                    fireballMaker.fullPower = false;
                    fireBallCounter++;
                    if (fireBallCounter >= 15)
                    {
                        fireballMaker.rotateDirection = fireballMaker.rotateDirection * -1;
                        fireBallCounter = 0;
                    }
                    break;
                case 2:
                    fireballMaker.attack = false;
                    if (jumpCounter > 3)
                    {
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
                                float distance = Vector2.Distance(new Vector2(randomX, randomY), playerPos);
                                if (distance < 2)
                                {
                                    atLeastOnePlayerNear = true;
                                }
                                if (Mathf.Sqrt(randomX * randomX + randomY * randomY ) > 7.0f)
                                {
                                    atLeastOnePlayerNear = true;
                                }

                                nearplayer = atLeastOnePlayerNear;
                            }
                        }

                        gameObject.transform.position = new Vector3(randomX, randomY, 0);

                        Projectile fb1 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90), this.gameObject.transform);
                        fb1.setDirection(new Vector2(1, 0));

                        Projectile fb2 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90), this.gameObject.transform);
                        fb2.setDirection(new Vector2(0, 1));

                        Projectile fb3 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90), this.gameObject.transform);
                        fb3.setDirection(new Vector2(-1, 0));

                        Projectile fb4 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90), this.gameObject.transform);
                        fb4.setDirection(new Vector2(0, -1));
                    }
                    jumpCounter++;
                    if (jumpCounter >= 30)
                    {
                        phaseCounter = 3;
                    }
                    break;
                case 3:
                    gameObject.transform.position = new Vector3(m_Middle.x, m_Middle.y, 0);
                    fireballMaker.fullPower = true;
                    fireballMaker.startAttacking(true);
                    fireBallCounter = 0;
                    fireBallCounter++;
                    if (fireBallCounter >= 15)
                    {
                        fireballMaker.rotateDirection = fireballMaker.rotateDirection * -1;
                        fireBallCounter = 0;
                    }
                    break;
                case 4:
                    fireballMaker.attack = false;
                    fireBallCounter = -2;
                    if (fireBallCounter >= 0)
                    {
                        m_RainFireBall.gameObject.GetComponent<RainProjectiles>().rain = true;
                    }
                    fireBallCounter++;
                    if (fireBallCounter >= 15)
                    {
                        m_RainFireBall.gameObject.GetComponent<RainProjectiles>().rain = false;
                        fireBallCounter = -5;
                    }
                    fireBallCounter = -3;
                    break;
                case 5:
                    if (fireBallCounter >= 0)
                    {
                        m_RainFireBall.gameObject.GetComponent<RainProjectiles>().rain = true;
                        m_RainFireBall.gameObject.GetComponent<RainProjectiles>().m_SpawnTime = 1f;
                        fireballMaker.startAttacking(true);
                        fireballMaker.fullPower = true;
                        fireballMaker.rotateSpeed = 12;
                        fireBallCounter = 0;
                        
                        if (fireBallCounter >= 15)
                        {
                            fireballMaker.rotateDirection = fireballMaker.rotateDirection * -1;
                            fireBallCounter = 0;
                        }
                    }
                    fireBallCounter++;



                    break;
            }



        }


        private void Update()
        {
            if (!started)
            {
                GameObject[] playersToFind = GameObject.FindGameObjectsWithTag("Hero");

                for (int i = 0; i < playersToFind.Length; i++)
                {
                    Vector2 playerPos = playersToFind[i].transform.position;
                    Vector2 currPos = this.transform.position;
                    float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), playerPos);
                    if (distance < 10)
                    {
                        started = true;
                        InvokeRepeating("StartPlaying", 2.0f, 1.0f);
                        break;
                    }
                }
                    
            }
            else
            {
                if (m_MaxHP * 1 / 4 > m_HP)
                {
                    phaseCounter = 5;
                }
                else if (m_MaxHP * 2 / 4 > m_HP)
                {
                    phaseCounter = 4;
                }
                else if (m_MaxHP * 3 / 4 > m_HP)
                {
                    phaseCounter = 2;
                }
            }
            


            
        }

        public override void Die()
        {
            MobDeathEventInfo udei = new MobDeathEventInfo();
            udei.Unit = this;
            udei.Killer = this.killer;
            udei.Level = this.m_Level;
            EventSystem.Current.FireEvent(udei);
            Instantiate(m_Teleporter, transform.position, Quaternion.Euler(0,0,0));

            Destroy(this.gameObject);
        }

    }

}

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

        private bool readyToPhase3;

        private bool started;
        private bool readyToPhase5;

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
            readyToPhase3 = false;
            readyToPhase5 = false;
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
            healthBar.sizeDelta = new Vector2(m_HP/m_MaxHP*300, healthBar.sizeDelta.y);
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
                                if (Mathf.Sqrt((randomX-m_Middle.x) * (randomX - m_Middle.x) + (randomY - m_Middle.y) * (randomY - m_Middle.y)) > 7.0f)
                                {
                                    atLeastOnePlayerNear = true;
                                }

                                nearplayer = atLeastOnePlayerNear;
                            }
                        }
                        gameObject.transform.position = new Vector3(randomX, randomY, 0);

                        Projectile fb1 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
                        fb1.setDirection(new Vector2(1, 0));

                        Projectile fb2 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
                        fb2.setDirection(new Vector2(0, 1));

                        Projectile fb3 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
                        fb3.setDirection(new Vector2(-1, 0));

                        Projectile fb4 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
                        fb4.setDirection(new Vector2(0, -1));

                        if (jumpCounter >= 20)
                        {
                            Projectile fb5 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
                            fb5.setDirection(new Vector2(0.7f, 0.7f));

                            Projectile fb6 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
                            fb6.setDirection(new Vector2(-0.7f, 0.7f));

                            Projectile fb7 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
                            fb7.setDirection(new Vector2(0.7f, -0.7f));

                            Projectile fb8 = Instantiate(m_EnemyFireBall, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
                            fb8.setDirection(new Vector2(-0.7f, -0.7f));
                        }
                    }

                    jumpCounter++;
                    if (jumpCounter >= 30)
                    {
                        readyToPhase3 = true;
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
                        fireBallCounter = -2;
                    }
                    break;
                case 4:
                    fireballMaker.attack = false;
                    if (fireBallCounter >= 0)
                    {
                        m_RainFireBall.gameObject.GetComponent<RainProjectiles>().rain = true;
                    }
                    fireBallCounter++;
                    if (fireBallCounter >= 15)
                    {
                        m_RainFireBall.gameObject.GetComponent<RainProjectiles>().rain = false;
                        fireBallCounter = -3;
                    }
                    break;
                case 5:
                    if (!readyToPhase5)
                    {
                        m_RainFireBall.gameObject.GetComponent<RainProjectiles>().m_SpawnTime = 1.0f;
                        m_RainFireBall.gameObject.GetComponent<RainProjectiles>().ResetInvoke();
                        m_RainFireBall.gameObject.GetComponent<RainProjectiles>().rain = true;
                        readyToPhase5 = true;
                        fireBallCounter = -2;
                    }
                    if (fireBallCounter >= 0)
                    {
                        fireballMaker.startAttacking(true);
                        fireballMaker.fullPower = true;
                        fireballMaker.rotateSpeed = 10;
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
                else if (readyToPhase3)
                {
                    phaseCounter = 3;
                }
                else if (m_MaxHP * 3 / 4 > m_HP)
                {
                    phaseCounter = 2;
                }
            }

            Debug.Log(phaseCounter.ToString());


            
        }

        public override void Die()
        {
            MobDeathEventInfo udei = new MobDeathEventInfo();
            udei.Unit = this;
            udei.Killer = this.killer;
            udei.Level = this.m_Level;
            EventSystem.Current.FireEvent(udei);
            GameObject teleporter = Instantiate(m_Teleporter, transform.position, Quaternion.Euler(0,0,0));
            teleporter.SetActive(true);

            Destroy(this.gameObject);
        }

    }

}

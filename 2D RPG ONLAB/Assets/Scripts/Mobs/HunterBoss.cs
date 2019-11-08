using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EventCallbacks
{
    public class HunterBoss : Mobs
    {
        public List<ProjectileShooterAround> m_ArrowShootersTop;
        public List<ProjectileShooterAround> m_ArrowShootersLeft;
        public List<ProjectileShooterAround> m_ArrowShootersRight;
        public List<ProjectileShooterAround> m_ArrowShootersBot;

        private List<ProjectileShooterAround> m_AllShooters;


        public GameObject m_TopDefender;
        public GameObject m_LeftDefender;
        public GameObject m_RightDefender;
        public GameObject m_BotDefender;


        public Vector2 m_TopPosition;
        public Vector2 m_LeftPosition;
        public Vector2 m_RightPosition;
        public Vector2 m_BotPosition;

        public Vector2 m_MiddlePosition;

        public Projectile m_EnemyArrow;
        private int phaseCounter;

        private bool started;

        private int counter;
        private int outSideCounter;

        // Start is called before the first frame update
        void Start()
        {
            m_AllShooters = new List<ProjectileShooterAround>();

            for (int i = 0; i < m_ArrowShootersTop.Count; i++)
            {
                m_AllShooters.Add(m_ArrowShootersTop[i]);
            }
            for (int i = 0; i < m_ArrowShootersLeft.Count; i++)
            {
                m_AllShooters.Add(m_ArrowShootersLeft[i]);
            }
            for (int i = 0; i < m_ArrowShootersRight.Count; i++)
            {
                m_AllShooters.Add(m_ArrowShootersRight[i]);
            }
            for (int i = 0; i < m_ArrowShootersBot.Count; i++)
            {
                m_AllShooters.Add(m_ArrowShootersBot[i]);
            }

            outSideCounter = 0;
            counter = -1;
            started = false;
            phaseCounter = 1;
        }

        private void DoStuff()
        {
            switch (phaseCounter)
            {
                case 1:

                    float degrees = Random.Range(0, 45);

                    Projectile fb1 = Instantiate(m_EnemyArrow, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb1.setDirection(RotateVector2(new Vector2(1, 0), degrees));

                    Projectile fb2 = Instantiate(m_EnemyArrow, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb2.setDirection(RotateVector2(new Vector2(0, 1),degrees));

                    Projectile fb3 = Instantiate(m_EnemyArrow, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb3.setDirection(RotateVector2(new Vector2(-1, 0), degrees));

                    Projectile fb4 = Instantiate(m_EnemyArrow, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb4.setDirection(RotateVector2(new Vector2(0, -1), degrees));

                    Projectile fb5 = Instantiate(m_EnemyArrow, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb5.setDirection(RotateVector2(new Vector2(0.7f, 0.7f), degrees));

                    Projectile fb6 = Instantiate(m_EnemyArrow, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb6.setDirection(RotateVector2(new Vector2(-0.7f, 0.7f), degrees));

                    Projectile fb7 = Instantiate(m_EnemyArrow, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb7.setDirection(RotateVector2(new Vector2(0.7f, -0.7f), degrees));

                    Projectile fb8 = Instantiate(m_EnemyArrow, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb8.setDirection(RotateVector2(new Vector2(-0.7f, -0.7f), degrees));

                    break;

                case 2:

                    if (outSideCounter >= 5)
                    {
                        SetBossNewPositionRandom(5);
                        transform.position = new Vector3(m_MiddlePosition.x, m_MiddlePosition.y, 0);
                        outSideCounter = 0;
                        counter = -7;
                    }
                    if (counter == -1)
                    {
                        int randomInt = Random.Range(0, 5);
                        SetBossNewPositionRandom(randomInt);
                        outSideCounter++;
                    }

                    if (counter >=6)
                    {
                        int randomInt = Random.Range(0, 5);
                        SetBossNewPositionRandom(randomInt);
                        counter = -1;
                        outSideCounter++;
                    }
                    counter++;
                   // Debug.Log(counter);
                    break;

                case 3:
                    float degree = Random.Range(0, 45);

                    Projectile fb11 = Instantiate(m_EnemyArrow, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb11.setDirection(RotateVector2(new Vector2(1, 0), degree));

                    Projectile fb21 = Instantiate(m_EnemyArrow, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb21.setDirection(RotateVector2(new Vector2(0, 1), degree));

                    Projectile fb31 = Instantiate(m_EnemyArrow, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb31.setDirection(RotateVector2(new Vector2(-1, 0), degree));

                    Projectile fb41 = Instantiate(m_EnemyArrow, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb41.setDirection(RotateVector2(new Vector2(0, -1), degree));

                    Projectile fb51 = Instantiate(m_EnemyArrow, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb51.setDirection(RotateVector2(new Vector2(0.7f, 0.7f), degree));

                    Projectile fb61 = Instantiate(m_EnemyArrow, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb61.setDirection(RotateVector2(new Vector2(-0.7f, 0.7f), degree));

                    Projectile fb71 = Instantiate(m_EnemyArrow, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb71.setDirection(RotateVector2(new Vector2(0.7f, -0.7f), degree));

                    Projectile fb81 = Instantiate(m_EnemyArrow, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 180));
                    fb81.setDirection(RotateVector2(new Vector2(-0.7f, -0.7f), degree));

                    if (counter % 3 == 0)
                    {
                        for (int i = 0; i < m_AllShooters.Count; i++)
                        {
                            m_AllShooters[i].ResetShootRate(1.0f, 200.0f);
                            m_AllShooters[i].gameObject.SetActive(false);
                            m_AllShooters[i].shootingEnabled = false;
                        }

                        int rand1 = Random.Range(0, m_AllShooters.Count);
                        int rand2 = Random.Range(0, m_AllShooters.Count);
                        int rand3 = Random.Range(0, m_AllShooters.Count);
                        int rand4 = Random.Range(0, m_AllShooters.Count);
                        int rand5 = Random.Range(0, m_AllShooters.Count);

                        m_AllShooters[rand1].gameObject.SetActive(true);
                        m_AllShooters[rand2].gameObject.SetActive(true);
                        m_AllShooters[rand3].gameObject.SetActive(true);
                        m_AllShooters[rand4].gameObject.SetActive(true);
                        m_AllShooters[rand5].gameObject.SetActive(true);
                        m_AllShooters[rand1].shootingEnabled = true;
                        m_AllShooters[rand2].shootingEnabled = true;
                        m_AllShooters[rand3].shootingEnabled = true;
                        m_AllShooters[rand4].shootingEnabled = true;
                        m_AllShooters[rand5].shootingEnabled = true;

                    }
                    counter++;
                    break;
            }
        }


        private void SetBossNewPositionRandom(int i)
        {
            switch (i)
            {
                case 1:
                    transform.position = new Vector3(m_TopPosition.x, m_TopPosition.y, 0);
                    m_TopDefender.SetActive(true);
                    m_LeftDefender.SetActive(false);
                    m_RightDefender.SetActive(false);
                    m_BotDefender.SetActive(false);
                    for (int j = 0; j < m_ArrowShootersTop.Count; j++)
                    {
                        m_ArrowShootersTop[j].gameObject.SetActive(true);
                        m_ArrowShootersTop[j].ResetShootRate(4.0f, 200.0f);
                        m_ArrowShootersTop[j].shootingEnabled = true;
                    }
                    for (int j = 0; j < m_ArrowShootersLeft.Count; j++)
                    {
                        m_ArrowShootersLeft[j].gameObject.SetActive(false);
                        m_ArrowShootersLeft[j].shootingEnabled = false;
                    }
                    for (int j = 0; j < m_ArrowShootersRight.Count; j++)
                    {
                        m_ArrowShootersRight[j].gameObject.SetActive(false);
                        m_ArrowShootersRight[j].shootingEnabled = false;
                    }
                    for (int j = 0; j < m_ArrowShootersBot.Count; j++)
                    {
                        m_ArrowShootersBot[j].gameObject.SetActive(false);
                        m_ArrowShootersBot[j].shootingEnabled = false;
                    }
                    break;
                case 2:
                    transform.position = new Vector3(m_LeftPosition.x, m_LeftPosition.y, 0);
                    m_TopDefender.SetActive(false);
                    m_LeftDefender.SetActive(true);
                    m_RightDefender.SetActive(false);
                    m_BotDefender.SetActive(false);
                    for (int j = 0; j < m_ArrowShootersTop.Count; j++)
                    {
                        m_ArrowShootersTop[j].gameObject.SetActive(false);
                        m_ArrowShootersTop[j].shootingEnabled = false;

                    }
                    for (int j = 0; j < m_ArrowShootersLeft.Count; j++)
                    {
                        m_ArrowShootersLeft[j].gameObject.SetActive(true);
                        m_ArrowShootersLeft[j].ResetShootRate(4.0f, 200.0f);
                        m_ArrowShootersLeft[j].shootingEnabled = true;
                    }
                    for (int j = 0; j < m_ArrowShootersRight.Count; j++)
                    {
                        m_ArrowShootersRight[j].gameObject.SetActive(false);
                        m_ArrowShootersRight[j].shootingEnabled = false;
                    }
                    for (int j = 0; j < m_ArrowShootersBot.Count; j++)
                    {
                        m_ArrowShootersBot[j].gameObject.SetActive(false);
                        m_ArrowShootersBot[j].shootingEnabled = false;
                    }
                    break;
                case 3:
                    transform.position = new Vector3(m_RightPosition.x, m_RightPosition.y, 0);
                    m_TopDefender.SetActive(false);
                    m_LeftDefender.SetActive(false);
                    m_RightDefender.SetActive(true);
                    m_BotDefender.SetActive(false);
                    for (int j = 0; j < m_ArrowShootersTop.Count; j++)
                    {
                        m_ArrowShootersTop[j].gameObject.SetActive(false);
                        m_ArrowShootersTop[j].shootingEnabled = false;
                    }
                    for (int j = 0; j < m_ArrowShootersLeft.Count; j++)
                    {
                        m_ArrowShootersLeft[j].gameObject.SetActive(false);
                        m_ArrowShootersLeft[j].shootingEnabled = false;
                    }
                    for (int j = 0; j < m_ArrowShootersRight.Count; j++)
                    {
                        m_ArrowShootersRight[j].gameObject.SetActive(true);
                        m_ArrowShootersRight[j].ResetShootRate(4.0f, 200.0f);
                        m_ArrowShootersRight[j].shootingEnabled = true;
                    }
                    for (int j = 0; j < m_ArrowShootersBot.Count; j++)
                    {
                        m_ArrowShootersBot[j].gameObject.SetActive(false);
                        m_ArrowShootersBot[j].shootingEnabled = false;
                    }
                    break;
                case 4:
                    transform.position = new Vector3(m_BotPosition.x, m_BotPosition.y, 0);
                    m_TopDefender.SetActive(false);
                    m_LeftDefender.SetActive(false);
                    m_RightDefender.SetActive(false);
                    m_BotDefender.SetActive(true);
                    for (int j = 0; j < m_ArrowShootersTop.Count; j++)
                    {
                        m_ArrowShootersTop[j].gameObject.SetActive(false);
                        m_ArrowShootersTop[j].shootingEnabled = false;
                    }
                    for (int j = 0; j < m_ArrowShootersLeft.Count; j++)
                    {
                        m_ArrowShootersLeft[j].gameObject.SetActive(false);
                        m_ArrowShootersLeft[j].shootingEnabled = false;
                    }
                    for (int j = 0; j < m_ArrowShootersRight.Count; j++)
                    {
                        m_ArrowShootersRight[j].gameObject.SetActive(false);
                        m_ArrowShootersRight[j].shootingEnabled = false;
                    }
                    for (int j = 0; j < m_ArrowShootersBot.Count; j++)
                    {
                        m_ArrowShootersBot[j].gameObject.SetActive(true);
                        m_ArrowShootersBot[j].ResetShootRate(4.0f, 200.0f);
                        m_ArrowShootersBot[j].shootingEnabled = true;
                    }
                    break;
                case 5:
                    m_TopDefender.SetActive(false);
                    m_LeftDefender.SetActive(false);
                    m_RightDefender.SetActive(false);
                    m_BotDefender.SetActive(false);
                    for (int j = 0; j < m_ArrowShootersTop.Count; j++)
                    {
                        m_ArrowShootersTop[j].gameObject.SetActive(false);
                        m_ArrowShootersTop[j].shootingEnabled = false;
                    }
                    for (int j = 0; j < m_ArrowShootersLeft.Count; j++)
                    {
                        m_ArrowShootersLeft[j].gameObject.SetActive(false);
                        m_ArrowShootersLeft[j].shootingEnabled = false;
                    }
                    for (int j = 0; j < m_ArrowShootersRight.Count; j++)
                    {
                        m_ArrowShootersRight[j].gameObject.SetActive(false);
                        m_ArrowShootersRight[j].shootingEnabled = false;
                    }
                    for (int j = 0; j < m_ArrowShootersBot.Count; j++)
                    {
                        m_ArrowShootersBot[j].gameObject.SetActive(false);
                        m_ArrowShootersBot[j].shootingEnabled = false;
                    }
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
                    float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), playerPos);
                    if (distance < 10)
                    {
                        started = true;
                        InvokeRepeating("DoStuff", 1.0f, 1.0f);
                        break;
                    }
                }

            }
            else
            {
                if (m_MaxHP * 1 / 3 > m_HP)
                {
                    phaseCounter = 3;
                }
                else if (m_MaxHP * 2 / 3 > m_HP)
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
           // GameObject teleporter = Instantiate(m_Teleporter, transform.position, Quaternion.Euler(0, 0, 0));
          //  teleporter.SetActive(true);

            Destroy(this.gameObject);
        }


        private Vector2 RotateVector2(Vector2 v, float degrees)
        {
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            float tx = v.x;
            float ty = v.y;
            v.x = (cos * tx) - (sin * ty);
            v.y = (sin * tx) + (cos * ty);
            return v;
        }

       

        public override void ManageMovement()
        {
        }

        protected override void Movement(Vector2 Dir)
        {
        }

        public override void Attack(Vector2 Dir)
        {
        }
    }

}

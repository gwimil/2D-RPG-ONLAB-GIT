using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [HideInInspector] public static long ID = 1;
    [HideInInspector] public long id;
    [HideInInspector] public NeuralNetworkBProp attackNeuralNetwork;
    [HideInInspector] public NeuralNetwork dodgeNeuralNetwork;

    private float m_MovementSpeed = 3.0f;

    public const int maxHealth = 100;
    public int currentHealth = maxHealth;
    public RectTransform healthBar;


    public GameObject projectile;
    public GameObject m_FightProjectile;

    private Rigidbody2D rigidbody;

    public bool dodge;
    public bool attack;
    public bool fight;
    public bool waitingForAttackToReach;

    public bool canAttack;

    public float cooldownInSec;
    private float currentCoolDown;
    public float sizeOfArena;

    private Vector2 m_Direction;

    public float m_WallDistance = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        id = ID;
        ID++;
        InvokeRepeating("CanShoot", 0.0f, 1.0f);
    }

    private void CanShoot()
    {
        canAttack = true;
    }


    // Update is called once per frame
    void Update()
    {
        Quaternion q = transform.rotation;
        q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
        transform.rotation = q;
        if (dodge)
        {
            float up = 0;
            float down = 0;
            float right = 0;
            float left = 0;

            GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
            GameObject closestProjectile = null;
            float closestDistance = Mathf.Sqrt(sizeOfArena* sizeOfArena + sizeOfArena* sizeOfArena);
            for (int i = 0; i < projectiles.Length; i++)
            {
                if (closestProjectile == null)
                {
                    closestProjectile = projectiles[i];
                }
                else
                {
                    float distance = Vector2.Distance(projectiles[i].transform.position, this.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestProjectile = projectiles[i];
                    }
                }
            }
            float rightWallDistance = Vector2.Distance(this.transform.position, new Vector2(sizeOfArena / 2, this.transform.position.y));
            float leftWallDistance = Vector2.Distance(this.transform.position, new Vector2(-sizeOfArena/2, this.transform.position.y));
            float topWallDistance = Vector2.Distance(this.transform.position, new Vector2(this.transform.position.x, sizeOfArena / 2));
            float bottomWallDistance = Vector2.Distance(this.transform.position, new Vector2(this.transform.position.x, -sizeOfArena / 2));

            bool isWall = false;
            if (closestDistance > rightWallDistance )
            {
                closestDistance = rightWallDistance;
                right = 1;
                up = 0;
                left = 0;
                down = 0;
                isWall = true;
            }
            if (closestDistance > leftWallDistance)
            {
                closestDistance = leftWallDistance;
                right = 0;
                up = 0;
                left = 1;
                down = 0;
                isWall = true;

            }
            if (closestDistance > topWallDistance)
            {
                closestDistance = topWallDistance;
                right = 0;
                up = 1;
                left = 0;
                down = 0;
                isWall = true;
            }
            if (closestDistance > bottomWallDistance)
            {
                closestDistance = bottomWallDistance;
                right = 0;
                up = 0;
                left = 0;
                down = 1;
                isWall = true;
            }

            Debug.Log("right: " + rightWallDistance);
            Debug.Log("left: " + leftWallDistance);
            Debug.Log("top: " + topWallDistance);
            Debug.Log("bot: " + bottomWallDistance);
            Debug.Log("distance: " + closestDistance);

            if (!isWall)
            {
                if (closestProjectile.transform.position.x > this.transform.position.x)
                {
                    Vector2 line = closestProjectile.transform.position - this.transform.position;
                    float rightAngle = Vector2.Angle(new Vector2(1.0f, 0.0f).normalized, line.normalized);
                    right = rightAngle / 90.0f;
                    right = 1.0f - right;
                }
                if (closestProjectile.transform.position.x < this.transform.position.x)
                {
                    Vector2 line = closestProjectile.transform.position - this.transform.position;
                    float leftAngle = Vector2.Angle(new Vector2(-1.0f, 0.0f).normalized, line.normalized);
                    left = leftAngle / 90.0f;
                    left = 1.0f - left;
                }
                if (closestProjectile.transform.position.y > this.transform.position.y)
                {
                    Vector2 line = closestProjectile.transform.position - this.transform.position;
                    float upAngle = Vector2.Angle(new Vector2(0.0f, 1.0f).normalized, line.normalized);
                    up = upAngle / 90.0f;
                    up = 1.0f - up;
                }
                if (closestProjectile.transform.position.y < this.transform.position.y)
                {
                    Vector2 line = closestProjectile.transform.position - this.transform.position;
                    float downAngle = Vector2.Angle(new Vector2(0.0f, -1.0f).normalized, line.normalized);
                    down = downAngle / 90.0f;
                    down = 1.0f - down;
                }
            }

            if (m_WallDistance > rightWallDistance)
            {
                right = 1;
            }
            if (m_WallDistance > leftWallDistance)
            {
                left = 1;
            }
            if (m_WallDistance > topWallDistance)
            {
                up = 1;
            }
            if (m_WallDistance > bottomWallDistance)
            {
                down = 1;
            }


            closestDistance = Mathf.Min(closestDistance / Mathf.Sqrt(sizeOfArena * sizeOfArena + sizeOfArena * sizeOfArena), 1.0f);
            float[] results = dodgeNeuralNetwork.FeedForward(new float[] { closestDistance,up, down, right, left });
            float y = results[0] - results[1];
            float x = results[2] - results[3];
            if (x == 0 && y == 0)
            {
                x = 1;
                y = 1;
            }
            m_Direction = new Vector2(x, y).normalized;
        }


        if (attack)
        {
            GameObject hero = GameObject.FindGameObjectWithTag("Hero");
            Vector2 heroPosition = hero.transform.position;
            Vector2 heroMovement = hero.GetComponent<TestHero>().normalizedVelicoty;
            // float movementX = (heroMovement.x + 1.0f) / 2.0f;
            // float movementY = (heroMovement.y + 1.0f) / 2.0f;
            float movementX = heroMovement.x / 2.0f + 0.5f;
            float movementY = heroMovement.y / 2.0f + 0.5f;
            float distance = Vector2.Distance(heroPosition, this.transform.position);

            float up = 0;
            float down = 0;
            float right = 0;
            float left = 0;

            if (hero.transform.position.x > this.transform.position.x)
            {
                Vector2 line = hero.transform.position - this.transform.position;
                float rightAngle = Vector2.Angle(new Vector2(1.0f, 0.0f).normalized, line.normalized);
                right = rightAngle / 90.0f;
                right = 1.0f - right;
            }
            if (hero.transform.position.x < this.transform.position.x)
            {
                Vector2 line = hero.transform.position - this.transform.position;
                float leftAngle = Vector2.Angle(new Vector2(-1.0f, 0.0f).normalized, line.normalized);
                left = leftAngle / 90.0f;
                left = 1.0f - left;
            }
            if (hero.transform.position.y > this.transform.position.y)
            {
                Vector2 line = hero.transform.position - this.transform.position;
                float upAngle = Vector2.Angle(new Vector2(0.0f, 1.0f).normalized, line.normalized);
                up = upAngle / 90.0f;
                up = 1.0f - up;
            }
            if (hero.transform.position.y < this.transform.position.y)
            {
                Vector2 line = hero.transform.position - this.transform.position;
                float downAngle = Vector2.Angle(new Vector2(0.0f, -1.0f).normalized, line.normalized);
                down = downAngle / 90.0f;
                down = 1.0f - down;
            }

            float normalizedDistance = Mathf.Min(distance / Mathf.Sqrt(sizeOfArena * sizeOfArena + sizeOfArena * sizeOfArena), 1);
            Debug.Log("dis:" + normalizedDistance);
            Debug.Log("X:" + movementX);
            Debug.Log("Y:" + movementY);
            Debug.Log("u:" + up);
            Debug.Log("d:" + down);
            Debug.Log("r:" + right);
            Debug.Log("l:" + left);

            float[] results = attackNeuralNetwork.FeedForward(new float[] { normalizedDistance, movementX, movementY, up, down, right, left });
            float y = results[0] - results[1];
            float x = results[2] - results[3];
            if (x == 0 && y == 0)
            {
                x = 1;
                y = 1;
            }

            Vector2 normalizedMove = new Vector2(x, y).normalized;
            GameObject fProjectile = Instantiate(projectile, this.transform.position, Quaternion.Euler(0, 0, 90));
            fProjectile.GetComponent<NeuralProjectile>().setDirection(normalizedMove);


            waitingForAttackToReach = true;
            attack = false;
        }

        if (waitingForAttackToReach)
        {
            GameObject hero = GameObject.FindGameObjectWithTag("Hero");
            GameObject proj = GameObject.FindGameObjectWithTag("EnemyProjectile");
            float projectileDistance = Vector2.Distance(this.transform.position, proj.transform.position);
            float heroDistance = Vector2.Distance(this.transform.position, hero.transform.position);
            if (projectileDistance > heroDistance)
            {
                if (projectile != null)
                {
                    Destroy(proj.gameObject);
                }

                // corrigate values
                float up = 0;
                float down = 0;
                float right = 0;
                float left = 0;

                if (hero.transform.position.x > this.transform.position.x)
                {
                    Vector2 line = hero.transform.position - this.transform.position;
                    float rightAngle = Vector2.Angle(new Vector2(1.0f, 0.0f).normalized, line.normalized);
                    right = rightAngle / 90.0f;
                    right = 1.0f - right;
                }
                if (hero.transform.position.x < this.transform.position.x)
                {
                    Vector2 line = hero.transform.position - this.transform.position;
                    float leftAngle = Vector2.Angle(new Vector2(-1.0f, 0.0f).normalized, line.normalized);
                    left = leftAngle / 90.0f;
                    left = 1.0f - left;
                }
                if (hero.transform.position.y > this.transform.position.y)
                {
                    Vector2 line = hero.transform.position - this.transform.position;
                    float upAngle = Vector2.Angle(new Vector2(0.0f, 1.0f).normalized, line.normalized);
                    up = upAngle / 90.0f;
                    up = 1.0f - up;
                }
                if (hero.transform.position.y < this.transform.position.y)
                {
                    Vector2 line = hero.transform.position - this.transform.position;
                    float downAngle = Vector2.Angle(new Vector2(0.0f, -1.0f).normalized, line.normalized);
                    down = downAngle / 90.0f;
                    down = 1.0f - down;
                }

                attackNeuralNetwork.BackProp(new float[] { up, down, right, left });

                GameObject manager = GameObject.FindGameObjectWithTag("NeuralManager");
                Destroy(hero.gameObject);
                manager.GetComponent<NeuralManager>().RespawnTestHero();

                waitingForAttackToReach = false;
                attack = true;
            }
        }
        if (fight)
        {
            // dodging 
            float up = 0;
            float down = 0;
            float right = 0;
            float left = 0;

            GameObject[] projectiles = GameObject.FindGameObjectsWithTag("PlayerProjectile");
            GameObject heroobj = GameObject.FindGameObjectWithTag("Hero");
            GameObject closestProjectile = null;
            float closestDistance = Mathf.Sqrt(sizeOfArena * sizeOfArena + sizeOfArena * sizeOfArena);
            if (projectiles.Length != 0)
            {
                
                for (int i = 0; i < projectiles.Length; i++)
                {
                    if (closestProjectile == null)
                    {
                        closestProjectile = projectiles[i];
                    }
                    else
                    {
                        float distance = Vector2.Distance(projectiles[i].transform.position, this.transform.position);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestProjectile = projectiles[i];
                        }
                    }
                }
            }
           float heroDistance = Vector2.Distance(heroobj.transform.position, this.transform.position);
            if (closestDistance > heroDistance)
            {
                closestDistance = heroDistance;
                closestProjectile = heroobj;
            }


            float rightWallDistance = Vector2.Distance(this.transform.position, new Vector2(sizeOfArena / 2, this.transform.position.y));
            float leftWallDistance = Vector2.Distance(this.transform.position, new Vector2(-sizeOfArena / 2, this.transform.position.y));
            float topWallDistance = Vector2.Distance(this.transform.position, new Vector2(this.transform.position.x, sizeOfArena / 2));
            float bottomWallDistance = Vector2.Distance(this.transform.position, new Vector2(this.transform.position.x, -sizeOfArena / 2));

            bool isWall = false;
            if (closestDistance > rightWallDistance)
            {
                closestDistance = rightWallDistance;
                right = 1;
                up = 0;
                left = 0;
                down = 0;
                isWall = true;
            }
            if (closestDistance > leftWallDistance)
            {
                closestDistance = leftWallDistance;
                right = 0;
                up = 0;
                left = 1;
                down = 0;
                isWall = true;

            }
            if (closestDistance > topWallDistance)
            {
                closestDistance = topWallDistance;
                right = 0;
                up = 1;
                left = 0;
                down = 0;
                isWall = true;
            }
            if (closestDistance > bottomWallDistance)
            {
                closestDistance = bottomWallDistance;
                right = 0;
                up = 0;
                left = 0;
                down = 1;
                isWall = true;
            }

            if (!isWall)
            {
                if (closestProjectile.transform.position.x > this.transform.position.x)
                {
                    Vector2 line = closestProjectile.transform.position - this.transform.position;
                    float rightAngle = Vector2.Angle(new Vector2(1.0f, 0.0f).normalized, line.normalized);
                    right = rightAngle / 90.0f;
                    right = 1.0f - right;
                    
                }
                if (closestProjectile.transform.position.x < this.transform.position.x)
                {
                    Vector2 line = closestProjectile.transform.position - this.transform.position;
                    float leftAngle = Vector2.Angle(new Vector2(-1.0f, 0.0f).normalized, line.normalized);
                    left = leftAngle / 90.0f;
                    left = 1.0f - left;
                }
                if (closestProjectile.transform.position.y > this.transform.position.y)
                {
                    Vector2 line = closestProjectile.transform.position - this.transform.position;
                    float upAngle = Vector2.Angle(new Vector2(0.0f, 1.0f).normalized, line.normalized);
                    up = upAngle / 90.0f;
                    up = 1.0f - up;
                }
                if (closestProjectile.transform.position.y < this.transform.position.y)
                {
                    Vector2 line = closestProjectile.transform.position - this.transform.position;
                    float downAngle = Vector2.Angle(new Vector2(0.0f, -1.0f).normalized, line.normalized);
                    down = downAngle / 90.0f;
                    down = 1.0f - down;
                }
            }

            if (m_WallDistance > rightWallDistance)
            {
                right = 1;
            }
            if (m_WallDistance > leftWallDistance)
            {
                left = 1;
            }
            if (m_WallDistance > topWallDistance)
            {
                up = 1;
            }
            if (m_WallDistance > bottomWallDistance)
            {
                down = 1;
            }


            closestDistance = Mathf.Min(closestDistance / Mathf.Sqrt(sizeOfArena * sizeOfArena + sizeOfArena * sizeOfArena), 1.0f);
            float[] results = dodgeNeuralNetwork.FeedForward(new float[] { closestDistance, up, down, right, left });
            float y = results[0] - results[1];
            float x = results[2] - results[3];
            if (x == 0 && y == 0)
            {
                x = 1;
                y = 1;
            }
            m_Direction = new Vector2(x, y).normalized;

            // attacking
            if (canAttack)
            {
                canAttack = false;
                GameObject hero = GameObject.FindGameObjectWithTag("Hero");
                Vector2 heroPosition = hero.transform.position;
                Vector2 heroMovement = hero.GetComponent<NeuralMage>().velocity.normalized;
                float movementX = (heroMovement.x + 1.0f) / 2.0f;
                float movementY = (heroMovement.y + 1.0f) / 2.0f;
                float distance = Vector2.Distance(heroPosition, this.transform.position);

                up = 0;
                down = 0;
                right = 0;
                left = 0;

                if (hero.transform.position.x > this.transform.position.x)
                {
                    Vector2 line = hero.transform.position - this.transform.position;
                    float rightAngle = Vector2.Angle(new Vector2(1.0f, 0.0f).normalized, line.normalized);
                    right = rightAngle / 90.0f;
                    right = 1.0f - right;
                }
                if (hero.transform.position.x < this.transform.position.x)
                {
                    Vector2 line = hero.transform.position - this.transform.position;
                    float leftAngle = Vector2.Angle(new Vector2(-1.0f, 0.0f).normalized, line.normalized);
                    left = leftAngle / 90.0f;
                    left = 1.0f - left;
                }
                if (hero.transform.position.y > this.transform.position.y)
                {
                    Vector2 line = hero.transform.position - this.transform.position;
                    float upAngle = Vector2.Angle(new Vector2(0.0f, 1.0f).normalized, line.normalized);
                    up = upAngle / 90.0f;
                    up = 1.0f - up;
                }
                if (hero.transform.position.y < this.transform.position.y)
                {
                    Vector2 line = hero.transform.position - this.transform.position;
                    float downAngle = Vector2.Angle(new Vector2(0.0f, -1.0f).normalized, line.normalized);
                    down = downAngle / 90.0f;
                    down = 1.0f - down;
                }

                float normalizedDistance = Mathf.Min(distance / Mathf.Sqrt(sizeOfArena * sizeOfArena + sizeOfArena * sizeOfArena), 1);

                float[] nresults = attackNeuralNetwork.FeedForward(new float[] { normalizedDistance, movementX, movementY, up, down, right, left });
                y = nresults[0] - nresults[1];
                x = nresults[2] - nresults[3];
                if (x == 0 && y == 0)
                {
                    x = 1;
                    y = 1;
                }

                Vector2 normalizedMove = new Vector2(x, y).normalized;
                GameObject fProjectile = Instantiate(m_FightProjectile, this.transform.position, Quaternion.Euler(0, 0, 90));
                fProjectile.GetComponent<NeuralFightProjectile>().setDirection(normalizedMove);
            }


        }

    }


    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + m_Direction * Time.fixedDeltaTime * m_MovementSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!fight)
        {
            if ((collision.gameObject.tag == "Projectile" || collision.gameObject.tag == "Wall") && this.gameObject.tag == "Neural")
            {
                GameObject manager = GameObject.FindGameObjectWithTag("NeuralManager");
                manager.GetComponent<NeuralManager>().dodgeCopyNeural = new NeuralNetwork(this.dodgeNeuralNetwork);
                Destroy(this.gameObject);
            }
        }
    }



    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        OnChangeHealth(currentHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    void OnChangeHealth(int health)
    {
        healthBar.sizeDelta = new Vector2(health * 2, healthBar.sizeDelta.y);
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }





}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [HideInInspector] public static long ID = 1;
    [HideInInspector] public long id;
    [HideInInspector] public NeuralNetworkBProp attackNeuralNetwork;
    [HideInInspector] public NeuralNetwork dodgeNeuralNetwork;

    public GameObject projectile;

    private Rigidbody2D rigidbody;

    public bool dodge;
    public bool attack;
    public bool waitingForAttackToReach;

    public bool canAttack;

    public float cooldownInSec;
    private float currentCoolDown;
    public float sizeOfArena;

    private Vector2 m_Direction;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        id = ID;
        ID++;
        InvokeRepeating("UpdateCd", 0.0f, cooldownInSec);

    }

    private void UpdateCd()
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
                    float upAngle = Vector2.Angle(new Vector2(1.0f, 0.0f).normalized, line.normalized);
                    up = upAngle / 90.0f;
                }
                if (closestProjectile.transform.position.x < this.transform.position.x)
                {
                    Vector2 line = closestProjectile.transform.position - this.transform.position;
                    float downAngle = Vector2.Angle(new Vector2(-1.0f, 0.0f).normalized, line.normalized);
                    down = downAngle / 90.0f;
                }
                if (closestProjectile.transform.position.y > this.transform.position.y)
                {
                    Vector2 line = closestProjectile.transform.position - this.transform.position;
                    float rightAngle = Vector2.Angle(new Vector2(0.0f, 1.0f).normalized, line.normalized);
                    right = rightAngle / 90.0f;
                }
                if (closestProjectile.transform.position.y < this.transform.position.y)
                {
                    Vector2 line = closestProjectile.transform.position - this.transform.position;
                    float leftAngle = Vector2.Angle(new Vector2(0.0f, -1.0f).normalized, line.normalized);
                    left = leftAngle / 90.0f;
                }
            }

            // TODO falat itt nézni és ha 1 >, akkor adni arra az oldalra = 1 -et

            closestDistance = Mathf.Min(closestDistance / Mathf.Sqrt(sizeOfArena * sizeOfArena + sizeOfArena * sizeOfArena), 1.0f);
            float[] results = dodgeNeuralNetwork.FeedForward(new float[] { closestDistance,up, down, right, left });
            float x = results[0] - results[1];
            float y = results[2] - results[3];
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
            Vector2 heroMovement = new Vector2(1, 1);
            //TODO Vector2 heroMovement = hero.GetComponent<Hero>().velicoty;
            float distance = Vector2.Distance(heroPosition, this.transform.position);

            float up = 0;
            float down = 0;
            float right = 0;
            float left = 0;

            if (hero.transform.position.x > this.transform.position.x)
            {
                Vector2 line = hero.transform.position - this.transform.position;
                float upAngle = Vector2.Angle(new Vector2(1.0f, 0.0f).normalized, line.normalized);
                up = upAngle / 90.0f;
            }
            if (hero.transform.position.x < this.transform.position.x)
            {
                Vector2 line = hero.transform.position - this.transform.position;
                float downAngle = Vector2.Angle(new Vector2(-1.0f, 0.0f).normalized, line.normalized);
                down = downAngle / 90.0f;
            }
            if (hero.transform.position.y > this.transform.position.y)
            {
                Vector2 line = hero.transform.position - this.transform.position;
                float rightAngle = Vector2.Angle(new Vector2(0.0f, 1.0f).normalized, line.normalized);
                right = rightAngle / 90.0f;
            }
            if (hero.transform.position.y < this.transform.position.y)
            {
                Vector2 line = hero.transform.position - this.transform.position;
                float leftAngle = Vector2.Angle(new Vector2(0.0f, -1.0f).normalized, line.normalized);
                left = leftAngle / 90.0f;
            }

            float[] results = attackNeuralNetwork.FeedForward(new float[] { distance, heroMovement.x, heroMovement.y, up, down, right, left });
            float x = results[0] - results[1];
            float y = results[2] - results[3];
            if (x == 0 && y == 0)
            {
                x = 1;
                y = 1;
            }

            Vector2 normalizedMove = new Vector2(x, y).normalized;

            // TODO fire projectile at movement
            waitingForAttackToReach = true;
            attack = false;
        }

        if (waitingForAttackToReach)
        {
            GameObject hero = GameObject.FindGameObjectWithTag("Hero");
            float projectileDistance = Vector2.Distance(this.transform.position, projectile.transform.position);
            float heroDistance = Vector2.Distance(this.transform.position, hero.transform.position);
            if (projectileDistance < heroDistance  || projectile == null)
            {
                if (projectile != null)
                {
                    Destroy(projectile.gameObject);
                }
                GameObject manager = GameObject.FindGameObjectWithTag("NeuralManager");
                Destroy(hero.gameObject);
                manager.GetComponent<NeuralManager>().RespawnTestHero();

                waitingForAttackToReach = false;
                attack = true;
            }
        }
    }


    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + m_Direction * Time.fixedDeltaTime * 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Projectile" || collision.gameObject.tag == "Wall" ) && this.gameObject.tag == "Neural")
        {
            GameObject manager = GameObject.FindGameObjectWithTag("NeuralManager");
            manager.GetComponent<NeuralManager>().dodgeCopyNeural = new NeuralNetwork(this.dodgeNeuralNetwork);
            Destroy(this.gameObject);
        }
    }
}

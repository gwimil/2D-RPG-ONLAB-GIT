using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float m_damage;
    public float m_movemenetSpeed;
    private Vector2 m_Direction;
    private float m_LifeTime;
    private float m_MaxLifeTime;
    private Rigidbody2D rigidbody;
    private Transform parentTransform;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        m_LifeTime = 0;
        m_MaxLifeTime = 200;
    }

    public void setDirection(Vector2 m)
    {
        m_Direction = m;
        float angle = Vector2.SignedAngle(m, new Vector2(1, 0));
        transform.Rotate(0, 0, -angle);

    }


    private void Update()
    {
        rigidbody.MovePosition(rigidbody.position + m_Direction * 0.1f);
        m_LifeTime++;
        if (m_LifeTime > m_MaxLifeTime) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.tag == "PlayerProjectile")
        {
            if (other.tag == "Enemy")
            {
                other.GetComponent<Non_AgressiveEnemy>().TakeDamage(m_damage);
            }
        }
        else if (this.tag == "EnemyProjectile")
        {
            if (other.tag == "Player")
            {
                other.GetComponent<Hero>().TakeDamage(m_damage);
            }
        }

        Destroy(this.gameObject);
    }



}

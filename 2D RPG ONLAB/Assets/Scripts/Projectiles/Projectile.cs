using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public abstract class Projectile : MonoBehaviour
    {
        [HideInInspector] public string user;

        public float m_damage;
        public float m_MovemenetSpeed;
        private Vector2 m_Direction;
        public float m_LifeTime;
        private new Rigidbody2D rigidbody;
        private Transform parentTransform;

        private IEnumerator coroutine;


        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            m_MovemenetSpeed = m_MovemenetSpeed / 1000;
            coroutine = WaitToDie(m_LifeTime);
            StartCoroutine(coroutine);
        }

        private IEnumerator WaitToDie(float waitUntil)
        {
            yield return new WaitForSeconds(waitUntil);
            Destroy(this.gameObject);
        }


        public void setDirection(Vector2 m)
        {
            m_Direction = m;
            float angle = Vector2.SignedAngle(m, new Vector2(1, 0));
            transform.Rotate(0, 0, -angle);
        }




        private void FixedUpdate()
        {
            rigidbody.MovePosition(rigidbody.position + m_Direction * m_MovemenetSpeed);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (this.tag == "PlayerProjectile")
            {
                if (other.tag == "Enemy")
                {
                    other.GetComponent<Mobs>().TakeDamage(m_damage, this.user);
                }
                if (other.tag == "EnemySpawner")
                {
                    other.GetComponent<EnemySpawner>().TakeDamage(m_damage, this.user);
                }
            }
            else if (this.tag == "EnemyProjectile")
            {
                if (other.tag == "Hero")
                {
                    other.GetComponent<Hero>().TakeDamage(m_damage);
                }
                if (other.tag == "Bot")
                {
                    other.GetComponent<FireClosestEnemyBot>().TakeDamage(m_damage);
                }
            }

            Destroy(this.gameObject);
        }

    }
}
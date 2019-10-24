using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace EventCallbacks
{
    public abstract class ArenaProjectiles : NetworkBehaviour
    {
        [HideInInspector] public string user;

        public float m_damage;
        public float m_MovemenetSpeed;
        private Vector2 m_Direction;
        private float m_LifeTime;
        private float m_MaxLifeTime;
        private new Rigidbody2D rigidbody;
        private Transform parentTransform;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            m_LifeTime = 0;
            m_MaxLifeTime = 100;
            m_MovemenetSpeed = m_MovemenetSpeed / 1000;
        }

        public void setDirection(Vector2 m)
        {
            if (!hasAuthority)
            {
                return;
            }
            m_Direction = m;
            float angle = Vector2.SignedAngle(m, new Vector2(1, 0));
            transform.Rotate(0, 0, -angle);
        }


        private void Update()
        {
            if (!hasAuthority)
            {
                return;
            }
            CmdUpdateLife();
        }

        [Command]
        public void CmdUpdateLife()
        {
            m_LifeTime++;
            if (m_LifeTime > m_MaxLifeTime) Destroy(this.gameObject);
        }

        private void FixedUpdate()
        {
            if (!hasAuthority)
            {
                return;
            }
            CmdUpdateMovement();
        }

        [Command]
        public void CmdUpdateMovement()
        {
            rigidbody.MovePosition(rigidbody.position + m_Direction * m_MovemenetSpeed);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!hasAuthority)
            {
                return;
            }
            CmdCollision(other);
        }

        [Command]
        public void CmdCollision(Collider2D other)
        {
            if (this.tag == "PlayerProjectile")
            {
                if (other.tag == "Enemy")
                {
                    other.GetComponent<Mobs>().TakeDamage(m_damage, this.user);
                }
            }
            else if (this.tag == "EnemyProjectile")
            {
                if (other.tag == "Hero")
                {
                    other.GetComponent<Hero>().TakeDamage(m_damage);
                }
            }

            Destroy(this.gameObject);
        }


    }
}
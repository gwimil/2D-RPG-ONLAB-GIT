using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{

    public abstract class Mobs : MonoBehaviour
    {
        public float M_AttackCooldown;
        protected float AttackCooldownATM;
        public float m_MovementSpeed;

        [HideInInspector] public string killer;
        
        public Bag m_Drop;

        public bool m_Aura;
        public int m_Level;
        public int m_EXP;
        public float m_MaxHP;
        public float m_HP;
        public float m_Armor;
        public float m_MagicResist;

        private Color originalColor;
        private new SpriteRenderer renderer;
        protected new Rigidbody2D rigidbody;

        protected Vector3 startPosition;

        private float timer;
        private int previousSecond;

        private void Awake()
        {
            renderer = GetComponentInChildren<SpriteRenderer>();
            originalColor = renderer.color;
            rigidbody = GetComponent<Rigidbody2D>();
            startPosition = rigidbody.position;

            
        }

        private void Start()
        {
            AttackCooldownATM = 0.0f;
            m_MovementSpeed = m_MovementSpeed / 1000;
            previousSecond = 0;
            timer = 0.0f;
        }

        private void Update()
        {
            // FRAMES

            
            timer += Time.deltaTime;
            int second = Convert.ToInt32(timer % 60);


            // SECONDS
            if (second - previousSecond == 1)
            {
                previousSecond++;
                if (M_AttackCooldown > AttackCooldownATM) AttackCooldownATM++;
                if (second == 5000000)
                {
                    second = 0;
                    previousSecond = 0;
                }
            }
        }

        private void FixedUpdate()
        {
            ManageMovement();
        }


        public void TakeDamage(float dmg)
        {
            m_HP -= dmg;
            renderer.color = Color.red;
            Invoke("ResetColor", 0.2f);
            CheckIfDeath();
        }
        void ResetColor()
        {
            renderer.color = originalColor;
        }

        private void CheckIfDeath()
        {
            if (m_HP <= 0.0f) Die();
        }

        abstract public void ManageMovement();
        abstract protected void Movement(Vector2 Dir);
        abstract public void Attack(Vector2 Dir);

        public void Die()
        {
            Bag droppedBag = Instantiate(m_Drop, transform.position, Quaternion.Euler(0, 0, 0));
            droppedBag.gameObject.name = "Bag";


            UnitDeathEventInfo udei = new UnitDeathEventInfo();
            udei.EventDescription = "Unit " + this.tag + " has died";
            udei.Unit = this;
            udei.Killer = this.killer;
            udei.Level = this.m_Level;
            EventSystem.Current.FireEvent(udei);

            Destroy(this.gameObject);
        }
    }
}
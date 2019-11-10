using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace EventCallbacks
{
    public abstract class ArenaHeroes : NetworkBehaviour
    {
        [HideInInspector]public static int _ID = 1;
        [HideInInspector] public int ID;


        public const int maxHealth = 100;
        [SyncVar(hook = "OnChangeHealth")]public int currentHealth = maxHealth;
        public RectTransform healthBar;

        public int m_BaseDMG;



        private float m_MovementSpeed = 3.0f;

        public Image m_ImageCooldownSpellOne;

        public Text m_TextCooldownSpellOne;

        public float m_SpellOneCooldown;
        protected float spellOneCooldownATM;
        public float m_SpellOneManaCost;


        public float m_BasicAttackCooldown;
        protected float basicAttackCooldownATM;

        protected Vector2 m_NormalizedMovement;


        protected Rigidbody2D rigidbody;




        public Sprite[] m_MovementSprites;

        


        private float timer;
        private int previousSecond;


        private SpriteRenderer spriteRenderer;

        private Vector2 velocity;
        private Quaternion q;

        public void Start()
        {

            ID = _ID;
            _ID++;
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidbody = GetComponent<Rigidbody2D>();
            m_NormalizedMovement = new Vector2(1, 0);

            m_ImageCooldownSpellOne.fillAmount = 0;
            m_TextCooldownSpellOne.text = "";

            spellOneCooldownATM = m_SpellOneCooldown;
            basicAttackCooldownATM = m_BasicAttackCooldown;

            previousSecond = 0;
            timer = 0.0f;


            InvokeRepeating("UpdateCd", 0.0f, 1.0f);
            InvokeRepeating("UpdateBasic", 0.0f, 0.1f);
        }

        private void UpdateCd()
        {
            previousSecond++;

            if (m_SpellOneCooldown > spellOneCooldownATM) spellOneCooldownATM++;
            if (spellOneCooldownATM > m_SpellOneCooldown) spellOneCooldownATM = m_SpellOneCooldown;

            setSpellTextOnCD(m_TextCooldownSpellOne, m_SpellOneCooldown, spellOneCooldownATM);
        }

        private void UpdateBasic()
        {
            if (m_BasicAttackCooldown >= basicAttackCooldownATM) basicAttackCooldownATM += 0.1f;
        }

        void Update()
        {

            if (!hasAuthority)
            {
                return;
            }


            //EVERY FRAME

            if (Input.GetButtonDown("BasicAttackP1"))
            {
                Attack();
            }


            //USE SPELL 1
            if (Input.GetButtonDown("SpellOneP1"))
            {
                UseSkills(1);
            }



            velocity = new Vector2(Input.GetAxisRaw("Horizontal1"), Input.GetAxisRaw("Vertical1")).normalized * m_MovementSpeed;


            q = transform.rotation;
            q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
            transform.rotation = q;


            m_ImageCooldownSpellOne.fillAmount = (m_SpellOneCooldown - spellOneCooldownATM) / m_SpellOneCooldown;

        }

        // MOVING AND UI ---------------------------------------MOVING AND UI---------------------------------------------MOVING AND UI



        private void FixedUpdate()
        {
            if (transform.rotation.z != 0 || velocity != new Vector2(0.0f, 0.0f))
            {
                Move(velocity * Time.fixedDeltaTime, q);
            }

        }


        public void Move(Vector2 position, Quaternion q)
        {

            if (!hasAuthority)
            {
                return;
            }

            rigidbody.MovePosition(rigidbody.position + position);
            if (position.x != 0 || position.y != 0) m_NormalizedMovement = position.normalized;

            int spriteNumber = 0;
            if (m_MovementSprites != null)
            {
                if (m_NormalizedMovement.y == 1.0f)
                {
                    spriteRenderer.sprite = m_MovementSprites[2];
                    spriteNumber = 2;
                }
                else if (m_NormalizedMovement.y == -1.0f)
                {
                    spriteRenderer.sprite = m_MovementSprites[0];
                    spriteNumber = 0;
                }
                else if (m_NormalizedMovement.x == 1.0f)
                {
                    spriteRenderer.sprite = m_MovementSprites[1];
                    spriteRenderer.flipX = false;
                    spriteNumber = 1;
                }
                else if (m_NormalizedMovement.x == -1.0f)
                {
                    spriteRenderer.sprite = m_MovementSprites[1];
                    spriteRenderer.flipX = true;
                    spriteNumber = 3;
                }
            }



            CmdUpdateVelocity(position, q, spriteNumber);

        }


        [Command]
        void CmdUpdateVelocity(Vector2 position, Quaternion qu, int num)
        {
            q = qu;
            velocity = position;
            if (position.x != 0 || position.y != 0) m_NormalizedMovement = position.normalized;
            if (num != 3)
            {
                spriteRenderer.sprite = m_MovementSprites[num];
            }
            if (num == 3)
            {
                spriteRenderer.sprite = m_MovementSprites[1];
                spriteRenderer.flipX = true;
            }
            else if (num == 1)
            {
                spriteRenderer.flipX = false;
            }

            RpcUpdateVelocity(position, qu, num);
        }

        [ClientRpc]
        void RpcUpdateVelocity(Vector2 position, Quaternion qu, int num)
        {
            if (hasAuthority)
            {
                return;
            }
            if (position.x != 0 || position.y != 0) m_NormalizedMovement = position.normalized;
            velocity = position;
            q = qu;

            if (num != 3)
            {
                spriteRenderer.sprite = m_MovementSprites[num];
            }
            if (num == 3)
            {
                spriteRenderer.sprite = m_MovementSprites[1];
                spriteRenderer.flipX = true;
            }
            else if (num == 1)
            {
                spriteRenderer.flipX = false;
            }
        }




        protected void setSpellTextOnCD(Text cText, float cooldownMax, float cooldownATM)
        {
            if (!hasAuthority)
            {
                return;
            }
            if (cooldownMax <= cooldownATM) cText.text = "";
            else cText.text = ((int)(cooldownMax - cooldownATM)).ToString();
        }



        public void TakeDamage(int amount)
        {

            if (!isServer)
            {
                return;
            }

            currentHealth -= amount;
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


        abstract public void Attack();
        abstract public void UseSkills(int i);



        private void Die()
        {
            if (isLocalPlayer)
            {
                Debug.Log("you died");
                gameObject.GetComponentInParent<PlayerConnectionObject>().SetYouLoseActive();
            }

            gameObject.SetActive(false);
            CmdPlayerDied();
        }

        [Command]
        void CmdPlayerDied()
        {
            NetworkServer.Destroy(this.gameObject);
        }


    }
}

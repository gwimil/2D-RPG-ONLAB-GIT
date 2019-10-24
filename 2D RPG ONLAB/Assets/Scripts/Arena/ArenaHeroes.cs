using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace EventCallbacks
{
    public abstract class ArenaHeroes : NetworkBehaviour
    {

        private float m_MovementSpeed = 3.0f;

        public Image m_ImageCooldownBasic;
        public Image m_ImageCooldownSpellOne;

        public Image m_ImageCooldownSpellTwo;

        public Text m_TextCooldownBasic;
        public Text m_TextCooldownSpellOne;
        public Text m_TextCooldownSpellTwo;

        public float m_SpellOneCooldown;
        protected float spellOneCooldownATM;
        public float m_SpellOneManaCost;

        public float m_SpellTwoCooldown;
        protected float spellTwoCooldownATM;
        public float m_SpellTwoManaCost;


        public float m_BasicAttackCooldown;
        protected float basicAttackCooldownATM;


        protected Vector2 m_NormalizedMovement;

        public Slider m_HpSlider;
        public Slider m_ManaSlider;
        protected Color m_FullHealthColor = Color.green;
        protected Color m_ZeroHealthColor = Color.red;
        protected Color m_FullManaColor = Color.blue;
        protected Color m_ZeroManaColor = Color.grey;
        public Image m_FillHPImage;
        public Image m_FillManaImage;


        protected Rigidbody2D rigidbody;


        // change the maxmana and maxhp different for each hero
        public float m_MaxHP = 100f;
        public float m_MaxMana = 100f;

        public float m_BaseDMG;

        [SyncVar]
        protected float m_CurrentHP;

        [SyncVar]
        protected float m_CurrentMana;

        public float m_Armor;
        public float m_MagicResist;
        public float m_HealthRegen;
        public float m_ManaRegen;

        public Sprite[] m_MovementSprites;


        private float timer;
        private int previousSecond;

        private string heroObjectName;

        private SpriteRenderer spriteRenderer;

        private Vector2 velocity;
        private Quaternion q;

        public void Start()
        {

            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidbody = GetComponent<Rigidbody2D>();
            heroObjectName = this.gameObject.name;
            m_NormalizedMovement = new Vector2(1, 0);

            m_ImageCooldownBasic.fillAmount = 0;
            m_ImageCooldownSpellOne.fillAmount = 0;
            m_ImageCooldownSpellTwo.fillAmount = 0;
            m_TextCooldownBasic.text = "";
            m_TextCooldownSpellOne.text = "";
            m_TextCooldownSpellTwo.text = "";


            m_CurrentHP = m_MaxHP;
            m_CurrentMana = m_MaxMana;

            SetHealthUI();
            spellOneCooldownATM = m_SpellOneCooldown;
            basicAttackCooldownATM = m_BasicAttackCooldown;

            previousSecond = 0;
            timer = 0.0f;

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


            m_ImageCooldownBasic.fillAmount = (m_BasicAttackCooldown - basicAttackCooldownATM) / m_BasicAttackCooldown;
            m_ImageCooldownSpellOne.fillAmount = (m_SpellOneCooldown - spellOneCooldownATM) / m_SpellOneCooldown;
            if (m_BasicAttackCooldown >= basicAttackCooldownATM) basicAttackCooldownATM++;



            timer += Time.deltaTime;
            int second = Convert.ToInt32(timer % 5000);

            // EVERY SECOND
            if (second - previousSecond == 1)
            {
                previousSecond++;

                if (m_SpellOneCooldown > spellOneCooldownATM) spellOneCooldownATM++;
                if (spellOneCooldownATM > m_SpellOneCooldown) spellOneCooldownATM = m_SpellOneCooldown;
                // TODO the the spell cooldown will shorten 0-1 sec depending on when we press it, FIX IT
                if (m_MaxMana >= m_CurrentMana) m_CurrentMana += m_ManaRegen;
                if (m_CurrentMana > m_MaxMana) m_CurrentMana = m_MaxMana;
                if (m_MaxHP >= m_CurrentHP) m_CurrentHP += m_ManaRegen;
                if (m_CurrentHP > m_MaxHP) m_CurrentHP = m_MaxMana;

                setSpellTextOnCD(m_TextCooldownBasic, m_BasicAttackCooldown, basicAttackCooldownATM);
                setSpellTextOnCD(m_TextCooldownSpellOne, m_SpellOneCooldown, spellOneCooldownATM);

                SetHealthUI();
                if (second == 5000)
                {
                    timer = 0.0f;
                    previousSecond = 0;
                }
            }
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
                Debug.Log("Returns");
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



        protected void SetHealthUI()
        {
            if (!hasAuthority)
            {
                return;
            }

            m_HpSlider.value = m_CurrentHP / m_MaxHP;
            m_ManaSlider.value = m_CurrentMana / m_MaxMana;
            m_FillHPImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHP / m_MaxHP);
            m_FillManaImage.color = Color.Lerp(m_ZeroManaColor, m_FullManaColor, m_CurrentMana / m_MaxMana);
        }



        public void TakeDamage(float amount)
        {
            if (!hasAuthority)
            {
                return;
            }

            m_CurrentHP -= amount;
            SetHealthUI();
            if (m_CurrentHP <= 0) Die();
        }


        abstract public void Attack();
        abstract public void UseSkills(int i);




        public void HealHero(float heal)
        {
            if (!hasAuthority)
            {
                return;
            }

            m_CurrentHP += heal;
            if (m_CurrentHP > m_MaxHP)
            {
                m_CurrentHP = m_MaxHP;
            }
        }

        public void ManaHero(float mana)
        {
            if (!hasAuthority)
            {
                return;
            }

            m_CurrentMana += mana;
            if (m_CurrentMana > m_MaxMana)
            {
                m_CurrentMana = m_MaxMana;
            }
        }



        private void Die()
        {

        }
    }
}

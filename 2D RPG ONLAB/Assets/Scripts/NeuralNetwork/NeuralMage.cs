using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeuralMage : MonoBehaviour
{

    public NeuralFightProjectile m_FireBall;


    public const int maxHealth = 100;
    public int currentHealth = maxHealth;
    public RectTransform healthBar;

    public int m_BaseDMG;

    private float m_MovementSpeed = 3.0f;

    public Image m_ImageCooldownSpellOne;

    public Text m_TextCooldownSpellOne;

    public float m_SpellOneCooldown;
    protected float spellOneCooldownATM;

    protected Vector2 m_NormalizedMovement;


    protected Rigidbody2D rigidbody;

    public Sprite[] m_MovementSprites;

    private SpriteRenderer spriteRenderer;

    public Vector2 velocity;
    private Quaternion q;

    private void Attack()
    {
        if (m_SpellOneCooldown <= spellOneCooldownATM)
        {
            GameObject fb = Instantiate(m_FireBall.gameObject, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
            fb.gameObject.GetComponent<NeuralFightProjectile>().setDirection(m_NormalizedMovement);
            spellOneCooldownATM = 0.0f;
        }
    }

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        m_NormalizedMovement = new Vector2(1, 0);

        m_ImageCooldownSpellOne.fillAmount = 0;
        m_TextCooldownSpellOne.text = "";

        spellOneCooldownATM = m_SpellOneCooldown;


        InvokeRepeating("UpdateCd", 0.0f, 0.1f);
    }

    private void UpdateCd()
    {
        if (m_SpellOneCooldown > spellOneCooldownATM) spellOneCooldownATM = spellOneCooldownATM + 0.1f;
        if (spellOneCooldownATM > m_SpellOneCooldown) spellOneCooldownATM = m_SpellOneCooldown;

        setSpellTextOnCD(m_TextCooldownSpellOne, m_SpellOneCooldown, spellOneCooldownATM);
    }

    void Update()
    {
        //EVERY FRAME

        if (Input.GetButtonDown("BasicAttackP1"))
        {
            Attack();
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
            Move(velocity * Time.fixedDeltaTime);
        }

    }


    public void Move(Vector2 position)
    {

        rigidbody.MovePosition(rigidbody.position + position);
        if (position.x != 0 || position.y != 0) m_NormalizedMovement = position.normalized;

        if (m_MovementSprites != null)
        {
            if (m_NormalizedMovement.y == 1.0f)
            {
                spriteRenderer.sprite = m_MovementSprites[2];
            }
            else if (m_NormalizedMovement.y == -1.0f)
            {
                spriteRenderer.sprite = m_MovementSprites[0];
            }
            else if (m_NormalizedMovement.x == 1.0f)
            {
                spriteRenderer.sprite = m_MovementSprites[1];
                spriteRenderer.flipX = false;
            }
            else if (m_NormalizedMovement.x == -1.0f)
            {
                spriteRenderer.sprite = m_MovementSprites[1];
                spriteRenderer.flipX = true;
            }
        }
    }


    protected void setSpellTextOnCD(Text cText, float cooldownMax, float cooldownATM)
    {
        if (cooldownMax <= cooldownATM) cText.text = "";
        else cText.text = ((int)(cooldownMax - cooldownATM)).ToString();
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

    private void Die()
    {
        Destroy(this.gameObject);
    }



}

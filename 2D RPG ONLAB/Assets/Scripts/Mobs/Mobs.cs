using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mobs : MonoBehaviour
{
    public float M_AttackCooldown;
    protected float AttackCooldownATM;
    public float m_MovementSpeed;

    public ItemManager itemManager;
    public GameObject m_Drop;

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

    private void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = renderer.color;
        rigidbody = GetComponent<Rigidbody2D>();
        startPosition = rigidbody.position;
        AttackCooldownATM = 0.0f;
        m_MovementSpeed = m_MovementSpeed / 1000;
    }
    
    private void Update()
    {
        if (M_AttackCooldown > AttackCooldownATM) AttackCooldownATM++;
        ManageMovement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
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
    abstract public void Die();
    

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mobs : MonoBehaviour
{
    public ItemManager itemManager;
    public GameObject m_Drop;

    public bool m_Aura;
    public int m_Level;
    public int m_EXP;
    public float m_MaxHP;
    public float m_MaxMana;
    public float m_HP;
    public float m_Mana;
    public float m_Armor;
    public float m_MagicResist;
    public float m_Damage;

    private Color originalColor;
    private SpriteRenderer renderer;

    private void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = renderer.color;
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
    abstract public void Movement();
    abstract public void Attack();
    abstract public void Die();
    

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mobs : MonoBehaviour
{
    public bool m_Aura;
    public int m_Level;
    public int m_EXP;
    public float m_HP;
    public float m_Mana;
    public float m_Armor;
    public float m_MagicResist;
    public float m_Damage;

    abstract public void ManageMovement();
    abstract public void Movement();
    abstract public void Attack();
    abstract public void Die();

}

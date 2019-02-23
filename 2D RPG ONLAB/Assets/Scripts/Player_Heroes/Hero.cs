using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    public float m_baseDMG;
    public float m_HP;
    public float m_Mana;
    public float m_Armor;
    public float m_MagicResist;

    abstract public void Move();
    abstract public void Attack();
    abstract public void UseSkill(int i);
}

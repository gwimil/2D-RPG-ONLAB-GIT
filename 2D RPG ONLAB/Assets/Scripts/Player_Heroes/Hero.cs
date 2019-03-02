using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{

    // GAMEMANAGER SHOULD MAKE THE CAMERA GET THE HEROES AS TARGETS

    protected Rigidbody2D rigidbody;

    int lvl = 1;
    int exp = 0;
    int expNeeded = 100;

    public float m_BaseDMG;
    public float m_HP;
    public float m_Mana;
    public float m_Armor;
    public float m_MagicResist;
    

    abstract public void Move(Vector2 vector);
    abstract public void Attack();
    abstract public void UseSkill(int i);
}

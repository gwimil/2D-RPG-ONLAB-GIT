﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{

    // GAME MANAGER SETS THE HERO
    [HideInInspector] public Hero m_hero;

    
    Vector2 velocity;
    public float m_MovementSpeed;
    public int m_PlayerID = 0;
    

    void Update()
    {
        if (m_PlayerID == 0)
            Debug.Log("You forgot to give your players ID-s");
        else
            velocity = new Vector2(Input.GetAxisRaw("Horizontal" + m_PlayerID), Input.GetAxisRaw("Vertical" + m_PlayerID)).normalized * m_MovementSpeed;
    }
  
    void FixedUpdate()
    {
        //rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
        // call rigidbody.MovePosition for the hero
        m_hero.Move(velocity * Time.fixedDeltaTime);
    }
}

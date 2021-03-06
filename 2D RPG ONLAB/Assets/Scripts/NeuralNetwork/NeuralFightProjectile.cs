﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralFightProjectile : MonoBehaviour
{
    public int m_damage;
    public float m_MovemenetSpeed;
    private Vector2 m_Direction;
    public float m_LifeTime;
    private new Rigidbody2D rigidbody;
    private Transform parentTransform;

    private IEnumerator coroutine;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        m_MovemenetSpeed = m_MovemenetSpeed / 1000;
        coroutine = WaitToDie(m_LifeTime);
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitToDie(float waitUntil)
    {
        yield return new WaitForSeconds(waitUntil);
        Destroy(this.gameObject);
    }


    public void setDirection(Vector2 m)
    {
        m_Direction = m;
        float angle = Vector2.SignedAngle(m, new Vector2(1, 0));
        transform.Rotate(0, 0, -angle);
    }


    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + m_Direction * m_MovemenetSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.tag == "PlayerProjectile")
        {
            if (other.tag == "Neural")
            {
                other.GetComponent<Bot>().TakeDamage(m_damage);
            }
        }
        else if (this.tag == "EnemyProjectile")
        {
            if (other.tag == "Hero")
            {
                other.GetComponent<NeuralMage>().TakeDamage(m_damage);
            }
        }

        Destroy(this.gameObject);
    }
}

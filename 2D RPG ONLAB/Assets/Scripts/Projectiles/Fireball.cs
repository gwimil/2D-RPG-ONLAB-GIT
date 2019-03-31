using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float m_damage;
    public float m_movemenetSpeed;
    private Vector2 m_Direction;
    public int m_LifeTime;
    private Rigidbody2D rigidbody;
    private Transform parentTransform;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void setDirection(Vector2 m)
    {
        m_Direction = m;
        float angle = Vector2.Angle(m, new Vector2(1, 0));
        transform.Rotate(0, 0, angle);

    }


    private void Update()
    {
        rigidbody.MovePosition(rigidbody.position + m_Direction * 0.1f);
        m_LifeTime++;
        if (m_LifeTime > 1000) Destroy(this.gameObject);
    }
    

}

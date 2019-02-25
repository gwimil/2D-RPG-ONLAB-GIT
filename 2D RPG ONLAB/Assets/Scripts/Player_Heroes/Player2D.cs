using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    
    Rigidbody2D rigidbody;
    Vector2 velocity;
    public float m_MovementSpeed;
    public int m_PlayerID = 0;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Quaternion q = transform.rotation;
        q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
        transform.rotation = q;

        if (m_PlayerID == 0)
            Debug.Log("You forgot to give your players ID-s");
        else
            velocity = new Vector2(Input.GetAxisRaw("Horizontal" + m_PlayerID), Input.GetAxisRaw("Vertical" + m_PlayerID)).normalized * m_MovementSpeed;
    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
    }
}

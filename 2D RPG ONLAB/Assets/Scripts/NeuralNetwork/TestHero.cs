using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHero : MonoBehaviour
{

    public Vector2 normalizedVelicoty;
    private float m_MovementSpeed = 3.0f;

    private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion q = transform.rotation;
        q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
        transform.rotation = q;
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + normalizedVelicoty * Time.deltaTime * m_MovementSpeed);
    }

}

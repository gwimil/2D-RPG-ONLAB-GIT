using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHero : MonoBehaviour
{

    public Vector2 normalizedVelicoty;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // TODO move
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "NeuralProjectile")
        {
            Destroy(this);
        }
    }
}

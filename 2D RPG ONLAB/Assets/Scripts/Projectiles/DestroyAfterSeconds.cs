using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{

    public int m_SecondToDestroy;

    private int counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        InvokeRepeating("DestroyAfter", 0.0f, 1.0f);
    }

    // Update is called once per frame
    void DestroyAfter()
    {

        if (counter >= m_SecondToDestroy)
        {
            Destroy(this.gameObject);
        }
        counter++;
    }
}

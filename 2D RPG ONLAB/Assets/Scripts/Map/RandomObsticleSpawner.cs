using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObsticleSpawner : MonoBehaviour
{


    public GameObject m_ObjectToSpawn;
    public float m_SpawnRadius;
    public int m_NumberOfSpawnedObjects;

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < m_NumberOfSpawnedObjects; i++)
        {
            float x = Random.Range(-m_SpawnRadius, m_SpawnRadius);
            float y = Random.Range(-m_SpawnRadius, m_SpawnRadius);


            Instantiate(m_ObjectToSpawn, new Vector3(this.transform.position.x + x, this.transform.position.y + y, 0), transform.rotation, gameObject.transform);

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}

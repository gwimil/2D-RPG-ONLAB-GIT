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
            float x = UnityEngine.Random.Range(-m_SpawnRadius, m_SpawnRadius);
            float y = UnityEngine.Random.Range(-m_SpawnRadius, m_SpawnRadius);


            Instantiate(m_ObjectToSpawn, new Vector3(x, y, 0), transform.rotation, gameObject.transform);

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}

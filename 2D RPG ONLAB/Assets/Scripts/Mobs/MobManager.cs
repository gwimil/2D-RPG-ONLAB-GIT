using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour
{

    public Mobs[] m_EnemyTypes;
    public Player2D[] m_Players;
    private int numberOfSpawns;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnRandomEnemies");
        numberOfSpawns = 1;
    }

    void Update()
    {

    }


    IEnumerator SpawnRandomEnemies()
    {
        while (true)
        {
            numberOfSpawns = 1;
            Debug.Log("Number of spawns: " + numberOfSpawns);
            int typeOfEnemyToSpawn = Random.Range(0, m_EnemyTypes.Length-1);
            Debug.Log("Type of enemy to spawn: " + numberOfSpawns);
            for (int i = 0; i < numberOfSpawns; i++)
            {
                Vector2 playerPos = m_Players[Random.Range(0, MenuData.m_playerNumber-1)].m_hero.transform.position;
                Debug.Log("PlayerPosition: " + numberOfSpawns);
                Vector2 plusPosition;
                plusPosition.x = Random.Range(0.0f, 1.0f) > 0.5 ? Random.Range(6.0f, 10.0f) : Random.Range(-6.0f, -10.0f);
                plusPosition.y = Random.Range(0.0f, 1.0f) > 0.5 ? Random.Range(6.0f, 10.0f) : Random.Range(-6.0f, -10.0f);
                Instantiate(m_EnemyTypes[numberOfSpawns], playerPos + plusPosition, Quaternion.Euler(0, 0, 0),this.transform);
            }


              yield return new WaitForSeconds(10);

        }
    }


}

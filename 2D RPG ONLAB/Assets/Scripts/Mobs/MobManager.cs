using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour
{

    public Mobs[] m_EnemyTypes;
    public Player2D[] m_Players;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnRandomEnemies");
    }

    IEnumerator SpawnRandomEnemies()
    {
        while (true)
        {
            int numberOfSpawns = Random.Range(0, 5);
            int typeOfEnemyToSpawn = Random.Range(0, m_EnemyTypes.Length - 1);
            for (int i = 0; i < numberOfSpawns; i++)
            {
                Vector2 playerPos = m_Players[Random.Range(0, MenuData.m_playerNumber)].transform.position;
                Vector2 plusPosition;
                plusPosition.x = Random.Range(0, 1) > 0.5 ? Random.Range(6, 10) : Random.Range(-6, -10);
                plusPosition.y = Random.Range(0, 1) > 0.5 ? Random.Range(6, 10) : Random.Range(-6, -10);
                Instantiate(m_EnemyTypes[typeOfEnemyToSpawn], playerPos + plusPosition, Quaternion.Euler(0, 0, 0),transform);
            }


              yield return new WaitForSeconds(10);

        }
    }


}

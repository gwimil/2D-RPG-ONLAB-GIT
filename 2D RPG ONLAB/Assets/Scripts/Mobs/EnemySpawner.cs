using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public class EnemySpawner : MonoBehaviour
    {
        public Mobs[] m_EnemyTypes;
        public int m_NumberOfSpawns;
        public float m_SquareDistanceToSpawnIn;

        public float m_HP;

        private int numberOfEnemiesSpawned;
        private List<Mobs> mobsSpawned;

        private float speedToRotate;
        private GameObject enemyArray;


        private void Awake()
        {
            enemyArray = new GameObject();
            enemyArray.name = "EnemiesSpawnedBy " + gameObject.name;
            speedToRotate = 20.0f;
            numberOfEnemiesSpawned = 0;
            mobsSpawned = new List<Mobs>();
        }
        


        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine("SpawnRandomEnemies");
        }
        
    
        
        IEnumerator SpawnRandomEnemies()
        {
            while (true)
            {
                if (!(numberOfEnemiesSpawned >= 10))
                { 
                    for (int i = 0; i < m_NumberOfSpawns; i++)
                    {
                        int typeOfEnemyToSpawn = Random.Range(0, m_EnemyTypes.Length);
                        Vector2 positionToSpawn = new Vector2(Random.Range(-m_SquareDistanceToSpawnIn, m_SquareDistanceToSpawnIn), Random.Range(-m_SquareDistanceToSpawnIn, m_SquareDistanceToSpawnIn));
                        Mobs mobSpawned = Instantiate(m_EnemyTypes[typeOfEnemyToSpawn], new Vector2(transform.position.x, transform.position.y) + positionToSpawn, Quaternion.Euler(0, 0, 0), enemyArray.transform);
                        mobsSpawned.Add(mobSpawned);
                        mobSpawned.gameObject.name = m_EnemyTypes[typeOfEnemyToSpawn].gameObject.name;
                        numberOfEnemiesSpawned++;
                    }
                }
                
                yield return new WaitForSeconds(10);
            }
        }

        private void Update()
        {
            transform.Rotate(Vector3.forward * speedToRotate * Time.deltaTime);
        }

        public void TakeDamage(float f, string killer)
        {
            this.m_HP -= f;
            if (m_HP <= 0.0f)
            {
                for(int i = 0; i < mobsSpawned.Count; i++)
                {
                    if (mobsSpawned[i] != null) mobsSpawned[i].Die();
                }
                Destroy(enemyArray.gameObject);

                SpawnerDeathEventInfo udei = new SpawnerDeathEventInfo();
                udei.EventDescription = "A spawner has been destroyed.";
                udei.Killer = killer;
                udei.Level = 5;
                EventSystem.Current.FireEvent(udei);
                Destroy(this.gameObject);
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemySpawn;
    public float spawnTimer = 0;
    public float spawnDelay = 0;
    public int numberOfEnemies;
    public int maxEnemies;
    public EnemyAi enemyAi;

    private void Start()
    {
        RepeatSpawn();
    }

    public void RepeatSpawn()
    {
        InvokeRepeating("SpawnEnemy", spawnTimer, spawnDelay);
    }


    public void SpawnEnemy()
    {
        Instantiate(enemySpawn, transform.position, transform.rotation);
        numberOfEnemies++;

        if (numberOfEnemies >= maxEnemies)
        {
            CancelInvoke("SpawnEnemy");
        }

    }
}

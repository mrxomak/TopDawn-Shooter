using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    

    public Wawe[] wawes;
    public Enemy enemy;
    public int enemiesAlive = 0;

    Wawe currentWawe;
    int currentWaweNumber;

    int enemiesToSpawn;
    float nextSpawTime;

    void Start()
    {
        NextWawe();
        EnemyCapsuleHealth.OnDeath += OnEnemyDeath;
    }

    void FixedUpdate()
    {
        _SpawnEnemy();
    }

    void _SpawnEnemy()
    {
        if(enemiesToSpawn > 0 && Time.time > nextSpawTime)
        {
            enemiesToSpawn--;
            nextSpawTime = Time.time + currentWawe.timeBetweenSpawns;
            Enemy spawnedEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity);
        }
    }

    public void OnEnemyDeath()
    { 
        enemiesAlive--;
        if (enemiesAlive == 0)
        {
            NextWawe();
        }
    }
        
    void NextWawe()
    {
        print("wawe" + currentWaweNumber);
        if (currentWaweNumber < wawes.Length)
        {
            currentWawe = wawes[currentWaweNumber];
            currentWaweNumber++;

            enemiesToSpawn = currentWawe.enemyCount;
            enemiesAlive = enemiesToSpawn;
        }
    }

    [System.Serializable]
    public class Wawe
    {
        public int enemyCount;
        public float timeBetweenSpawns;
    }
}
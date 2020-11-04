using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Waves")]
    // Waves
    public Wave[] waves;
    private Wave currentWave;
    private byte currentWaveNumber;

    // Spawner properties
    private float nextSpawnTime;

    [Header("Enemy Types Available")]
    // Reference to all types of enemies
    public Enemy[] enemyTypes;
    private float chanceOfSpawn;

    [Header("Parent for Enemies")]
    public Transform parentObject;

    void Start()
    {
        chanceOfSpawn = 100.0f / enemyTypes.Length;
        NextWave();
    }

    void Update()
    {
        if (Time.time > nextSpawnTime && currentWave.currentEnemies < currentWave.maxEnemies)
        {
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawn;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        int randomNumber = Random.Range(0, 100);
        int enemyIndexToSpawn = (int)(randomNumber / chanceOfSpawn);
        Enemy spawnedEnemy = Instantiate(enemyTypes[enemyIndexToSpawn], Vector3.zero, Quaternion.identity, parentObject);
        spawnedEnemy.OnDeath += OnEnemyDeath;
        currentWave.currentEnemies++;
    }

    void OnEnemyDeath()
    {
        currentWave.currentEnemies--;
        currentWave.enemyToKill--;
        if (currentWave.enemyToKill == 0)
        {
            NextWave();
        }
    }

    void NextWave()
    {
        if (currentWaveNumber+1 <= waves.Length)
        {
            currentWave = waves[currentWaveNumber];
            currentWaveNumber++;
        }
    }
}

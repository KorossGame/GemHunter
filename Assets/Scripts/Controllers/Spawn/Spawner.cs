using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

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

    [Header("Parent for Enemies")]
    public Transform parentObject;

    [Header("Flags")]
    public bool active;
    
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (enemyTypes.Length > 0)
        {
            active = true;
            NextWave();

            // Set random seed
            Random.InitState((int)System.DateTime.Now.Ticks);
        }
    }

    void Update()
    {

        // Spawn the enemy if spawner is activated
        if (Time.time > nextSpawnTime && PlayerManager.instance.player && active && currentWave != null)
        {
            // Check if wave time passed and we need to call next wave
            if (Time.time > currentWave.timeToNextWave)
            {
                NextWave();
            }
            // Check if enemies count is less than max
            if (currentWave.currentEnemies < currentWave.maxEnemies)
            {
                nextSpawnTime = Time.time + currentWave.timeBetweenSpawn;
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        // Create random spawn point
        int maxSpawnPoints = (currentWave.spawnPoints.Length);
        if (maxSpawnPoints > 0)
        {
            int newPoint = Random.Range(0, maxSpawnPoints);
            SpawnPoint randomSpawnPoint = currentWave.spawnPoints[newPoint];

            // Generate spawn chances depending on position difference
            randomSpawnPoint.GenerateSpawnChances();

            // Generate random number
            int randomNumber = Random.Range(0, 100);
            int enemyIndexToSpawn = 0;

            // Get enemy index to spawn depending on partial sums
            for (int i = 0; i < enemyTypes.Length - 1; i++)
            {
                if (randomNumber <= randomSpawnPoint.spawnChance[i])
                {
                    enemyIndexToSpawn = i;
                    break;
                }
            }

            // Array of pointers for spawning enemies
            Enemy spawnedEnemy = Instantiate(enemyTypes[enemyIndexToSpawn], randomSpawnPoint.transform.position, Quaternion.identity, parentObject);
            spawnedEnemy.OnDeath += OnEnemyDeath;
            currentWave.currentEnemies++;
        }
    }

    void OnEnemyDeath()
    {
        currentWave.currentEnemies--;
        /*
        currentWave.enemyToKill--;
        if (currentWave.enemyToKill == 0)
        {
            NextWave();
        }
        */
    }

    void NextWave()
    {
        if (currentWaveNumber <= waves.Length)
        {
            // Till wave 0 there can not be any enemies
            if (currentWaveNumber != 0)
            {
                // We need to kill all enemies till set a new wave
                KillAllEnemies();
            }

            // Set current wave as new one
            currentWave = waves[currentWaveNumber];

            // Reset timer till next wave
            currentWave.timeToNextWave = Time.time + currentWave.timeToNextWave;

            // Increment current wave number
            currentWaveNumber++;
        }
    }

    public void KillAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemyObject in enemies)
        {
            Destroy(enemyObject);
        }
        currentWave.currentEnemies = 0;
    }
}

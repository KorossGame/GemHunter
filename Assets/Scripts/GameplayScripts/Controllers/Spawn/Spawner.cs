using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    [Header("Waves")]
    // Waves
    public Wave[] waves;
    private Wave currentWave;
    private byte totalWaves;
    private byte currentWaveNumber = 0;

    // Spawner properties
    private float nextSpawnTime;

    [Header("Enemy Types Available")]
    // Reference to all types of enemies
    public Enemy[] enemyTypes;

    [HideInInspector]
    // Control of spawner
    public bool active { get; set; } = false;
    
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Set random seed
        Random.InitState((int)System.DateTime.Now.Ticks);

        // Get total waves count
        totalWaves = (byte)(waves.Length - 1);
    }

    void Update()
    {
        // Spawn the enemy if spawner is activated
        if (Time.time > nextSpawnTime && active && enemyTypes.Length > 0)
        {
            if (currentWaveNumber != 0)
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
            else
            {
                NextWave();
            }
        }
    }

    void SpawnEnemy()
    {
        // Create random spawn point
        int maxSpawnPoints = currentWave.spawnPoints.Length - 1;
        if (maxSpawnPoints >= 0)
        {
            // Choose random point
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
            Enemy spawnedEnemy = Instantiate(enemyTypes[enemyIndexToSpawn], randomSpawnPoint.transform.position, Quaternion.identity, gameObject.transform);
            
            // On death call event
            spawnedEnemy.OnDeath += OnEnemyDeath;
            currentWave.currentEnemies++;
        }
    }

    void OnEnemyDeath()
    {
        currentWave.currentEnemies--;
        /* If there is target enemies to kill (not timer as it is done right now)
         * 
        currentWave.enemyToKill--;
        if (currentWave.enemyToKill == 0)
        {
            NextWave();
        }
        */
    }

    public void NextWave()
    {
        if (currentWaveNumber+1 <= totalWaves)
        {
            // Increment current Wave number
            currentWaveNumber++;

            // We need to kill all enemies till set a new wave
            StartCoroutine(KillAllEnemies());

            // Set current wave as new one
            currentWave = waves[currentWaveNumber];

            // Reset timer till next wave
            currentWave.timeToNextWave = Time.time + currentWave.timeToNextWave;
        }
        else
        {
            active = false;
            SceneManager.LoadScene("BossScene");
        }
    }

    public IEnumerator KillAllEnemies()
    {
        /* Might be improved by object pool */

        // Get every child of spawner object with tag enemy and destroy it
        foreach (Transform enemyObject in transform)
        {
            if (enemyObject.gameObject.CompareTag("Enemy")){
                Destroy(enemyObject.gameObject);
            }
        }

        // In case it is wave 0 - we dont have currentWave yet
        if (currentWave != null)
        {
            // Set current enemies count to 0
            currentWave.currentEnemies = 0;
        }

        // Delay between wave switching
        yield return new WaitForSeconds(3f);
    }
}

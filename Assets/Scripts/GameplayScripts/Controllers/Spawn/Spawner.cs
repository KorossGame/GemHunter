using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    public Text timerText;
    [SerializeField] private int nextTimeWave = 3;

    [Header("UI")]
    private Text currentWaveText;
    private Text nextWaveTimerText;

    [Header("Waves")]
    // Waves
    public Wave[] waves;
    private Wave currentWave;
    private byte totalWaves;
    private byte currentWaveNumber = 0;

    // Spawner properties
    private float nextSpawnTime;
    private float nextWaveTime;

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

    void FixedUpdate()
    {
        if (!active)
        {
            StartCoroutine(KillAllEnemies());
        }
    }

    void Update()
    {
        // Spawn the enemy if spawner is activated
        if (Time.time > nextSpawnTime && active && enemyTypes.Length > 0)
        {
            if (currentWaveNumber != 0)
            {
                // Check if wave time passed and we need to call next wave
                if (Time.time >= nextWaveTime)
                {
                    StartCoroutine(NextWave());
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
                StartCoroutine(NextWave());
                PlayerManager.instance.player.GetComponent<Player>().WaveUI.SetActive(true);
                currentWaveText = GameObject.Find("WaveCounterText").GetComponent<Text>();
                nextWaveTimerText = GameObject.Find("nextWaveTimerText").GetComponent<Text>();
            }
        }
    }

    void SpawnEnemy()
    {
        // Create random spawn point
        int maxSpawnPoints = currentWave.spawnPoints.Length;
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
            for (int i = 0; i < enemyTypes.Length; i++)
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

    public IEnumerator NextWave()
    {
        
        if (currentWaveNumber+1 <= totalWaves)
        {
            active = false;

            // Increment current Wave number
            currentWaveNumber++;

            // We need to kill all enemies till set a new wave
            StartCoroutine(KillAllEnemies());
            PlayerManager.instance.player.GetComponent<Player>().Heal(50);

            // Delay between switch
            timerText.gameObject.SetActive(true);
            for (int i = nextTimeWave; i > 0; i--)
            {
                timerText.text = i.ToString();
                yield return new WaitForSeconds(1f);
            }
            timerText.gameObject.SetActive(false);

            // Set current wave as new one
            currentWave = waves[currentWaveNumber];
            currentWaveText.text = currentWaveNumber.ToString()+" / "+ (waves.Length - 1).ToString();

            // Reset timer till next wave
            nextWaveTime = Time.time + currentWave.timeToNextWave;
            active = true;

            StartCoroutine(UpdateTimer());
        }
        else
        {
            active = false;

            // Delay between switch
            timerText.gameObject.SetActive(true);
            for (int i = nextTimeWave; i > 0; i--)
            {
                timerText.text = i.ToString();
                yield return new WaitForSeconds(1f);
            }
            timerText.gameObject.SetActive(false);

            // Load next scene
            SceneManager.LoadScene("LoadingScreen");
        }
    }

    private IEnumerator UpdateTimer()
    {
        while (Time.time < nextWaveTime)
        {
            nextWaveTimerText.text = "Next Wave: " + ((int)(nextWaveTime - Time.time)).ToString();
            yield return new WaitForSeconds(1f);
        }
    }

    public IEnumerator KillAllEnemies()
    {
        /* Might be improved by object pool */

        // Get every child of spawner object with tag enemy and destroy it
        foreach (Transform enemyObject in transform)
        {
            if (enemyObject.gameObject.CompareTag("Enemy")){
                enemyObject.GetComponent<Enemy>().Die();
            }
        }

        // In case it is wave 0 - we dont have currentWave yet
        if (currentWave != null)
        {
            // Set current enemies count to 0
            currentWave.currentEnemies = 0;
        }

        yield break;
    }

    public void ResetWaveNumber()
    {
        currentWaveNumber = 0;
    }
}

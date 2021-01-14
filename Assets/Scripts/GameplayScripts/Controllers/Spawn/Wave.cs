using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    // Max spawned enemies
    public float maxEnemies;
    
    // Current enemies count
    [HideInInspector]
    public float currentEnemies;

    // Delta time between spawns of enemies
    public float timeBetweenSpawn;
    
    // How much enemies need to die
    // public byte enemyToKill;

    // How many time till NextWave
    public float timeToNextWave;

    // Spawn points with dynamic spawn chances
    public SpawnPoint[] spawnPoints;
}
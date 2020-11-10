using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    // Max spawned enemies
    public float maxEnemies;
    // Current enemies count
    public float currentEnemies;
    // Delta time between spawns of enemies
    public float timeBetweenSpawn;
    // How much enemies need to die
    public byte enemyToKill;
    // Spawn points with dynamic spawn chances
    public SpawnPoint[] spawnPoints;
}
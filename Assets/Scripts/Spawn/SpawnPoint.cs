using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnPoint : MonoBehaviour
{

    // Distance to player
    private float distanceToPlayer;

    // Player reference
    private Transform playerTransform;

    // Chance of Spawning every enemy
    public float[] spawnChance;
    private int enemyTypes;

    // Distance Phases depending on which spawn chances going to be changed
    private float phase1 = 5;
    private float phase2 = 10;
    private float phase3 = 15;


    void Start()
    {
        // Get player transform
        playerTransform = PlayerManager.instance.player.transform;
        
        // Get lenght of enemy types array
        enemyTypes = GameObject.FindGameObjectWithTag("GameController").GetComponent<Spawner>().enemyTypes.Length;
        
        // Create new array to store spawn numbers
        spawnChance = new float[enemyTypes];
    }

    public void GenerateSpawnChances()
    {
        distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // 80:20 (melee-ranged)
        if (distanceToPlayer <= phase1)
        {
            // Spawn more melee enemies
            spawnChance[0] = 40;
            spawnChance[1] = 40;

            // All ranged enemies have equal chance
            for (int i = 2; i < enemyTypes; i++)
            {
                spawnChance[i] = 20 / (enemyTypes - 2);
            }
        }

        // 50:50 (melee-ranged)
        else if (distanceToPlayer <= phase2)
        {
            // Spawn with 50% melee enemy type
            spawnChance[0] = 25;
            spawnChance[1] = 25;

            // All ranged enemies have equal chance
            for (int i = 2; i < enemyTypes; i++)
            {
                spawnChance[i] = 50 / (enemyTypes - 2);
            }
        }

        // 30:70 (melee-ranged)
        else if (distanceToPlayer <= phase3)
        {
            // Spawn of melee enemy type is 30%
            spawnChance[0] = 15;
            spawnChance[1] = 15;

            // Spawn of ranged enemies are 70%
            for (int i = 2; i < enemyTypes; i++)
            {
                spawnChance[i] = 70 / (enemyTypes - 2);
            }
        }

        // 10:90 (melee-ranged)
        else
        {
            spawnChance[0] = 5;
            spawnChance[1] = 5;

            // All ranged enemies have equal chance
            for (int i = 2; i < enemyTypes; i++)
            {
                spawnChance[i] = 90 / (enemyTypes - 2);
            }
        }

        // Make an array of partilias sums
        generatePartialSum();
    }

    private void generatePartialSum()
    {
        float counter = 0;
        for (int i=0; i < spawnChance.Length; i++)
        {
            counter += spawnChance[i];
            spawnChance[i] = counter;
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Berserk : BossState
{
    private Transform nullAttackPoint;

    private int radius = 7;
    private List<Node> enemySpawnNodes;

    private byte maxSpawnedEnemies = 6;
    private byte currentSpawnedEnemies;

    public Berserk(BossFSM bossFSM, BossBullet projectile, Animator animator, NavMeshAgent pathFinder, Transform nullPoint) : base(bossFSM, projectile, animator, pathFinder)
    {
        delayAttackTime = 7.5f;
        nullAttackPoint = nullPoint;
    }

    public override IEnumerator Enter()
    {
        bossPathFinder.stoppingDistance = 200f;
        return base.Enter();
    }

    public override IEnumerator Attack()
    {
        if (nextAttackTime <= Time.time)
        {
            CheckNodes();
            RandomSpawnEnemy();
            nextAttackTime = Time.time + delayAttackTime;
        }
        return base.Attack();
    }

    private void CheckNodes()
    {
        enemySpawnNodes = new List<Node>();
        foreach (Node node in Grid.instance.nodeList)
        {
            // Check if nodes are in radius to boss, if so add them to the list
            if (
                (node.worldPosition.x <= nullAttackPoint.position.x + radius && node.worldPosition.x >= nullAttackPoint.position.x - radius)
                &&
                (node.worldPosition.z <= nullAttackPoint.position.z + radius && node.worldPosition.z >= nullAttackPoint.position.z - radius)
                &&
                (node.available)
               )
            {
                enemySpawnNodes.Add(node);
            }
        }
    }

    private void RandomSpawnEnemy()
    {
        if (currentSpawnedEnemies < maxSpawnedEnemies)
        {
            // generate random number of enemies
            int randomEnemyCount = Random.Range(1, maxSpawnedEnemies - currentSpawnedEnemies);

            for (int enemyCount = 0; enemyCount < randomEnemyCount; enemyCount++)
            {
                // Get random node
                int randomNode = Random.Range(0, enemySpawnNodes.Count);

                // Get random enemy type
                Enemy enemyObject = Spawner.instance.enemyTypes[Random.Range(0, Spawner.instance.enemyTypes.Length)];

                // Spawn
                Enemy e = UnityEngine.Object.Instantiate(enemyObject, enemySpawnNodes[randomNode].worldPosition, Quaternion.identity);
                e.OnDeath += DecrementSpawnedEnemiesCounter;

                // Increment spawned enemies counter
                currentSpawnedEnemies++;
            }
        }
    }

    private void DecrementSpawnedEnemiesCounter()
    {
        currentSpawnedEnemies--;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Nightmare : BossState
{
    private Transform nullAttackPoint;

    private float maxAngle = 45f;
    private int radius = 10;
    private int height = 30;
    private int meteorCount = 5;

    public Nightmare(BossFSM bossFSM, BossBullet projectile, Animator animator, NavMeshAgent pathFinder, Transform nullPoint) : base(bossFSM, projectile, animator, pathFinder)
    {
        delayAttackTime = 4f;
        nullAttackPoint = nullPoint;
    }

    public override IEnumerator Enter()
    { 
        return base.Enter();
    }

    public override IEnumerator Attack()
    {
        if (nextAttackTime <= Time.time)
        {
            nextAttackTime = Time.time + delayAttackTime;
            SpawnMeteor();
        }
        return base.Attack();
    }

    private void SpawnMeteor()
    {
        for (int i=0; i < meteorCount; i++)
        {
            // Generate different angles
            float angleX = Random.Range(0, maxAngle);
            float angleZ = Random.Range(0, maxAngle);

            // Get X and Z component offset
            float difX = Mathf.Tan(angleX * Mathf.Deg2Rad) * height;
            float difZ = Mathf.Tan(angleZ * Mathf.Deg2Rad) * height;

            // Target and Spawn Pos
            Vector3 targetPos = new Vector3(nullAttackPoint.position.x + Random.Range(-radius, radius), nullAttackPoint.position.y, nullAttackPoint.position.z + Random.Range(-radius, radius));
            Vector3 spawnPos = new Vector3(targetPos.x + difX, height, targetPos.z + difZ);

            /*
             * FOR TESTING PURPOSES
             * GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
             * cube.transform.position = targetPos;
            */

            // Spawn
            BossBullet ob = UnityEngine.Object.Instantiate(bossProjectile, spawnPos, Quaternion.identity);
            ob.transform.LookAt(targetPos);
        }
    }
}
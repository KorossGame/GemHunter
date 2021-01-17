using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rage : BossState
{
    private Transform bossZeroPoint;
    private Transform frontAttackPoint;
    private Boss bossReference;

    private float pointsCount = 20;
    private float angleToRotate;

    public Rage(BossFSM bossFSM, BossBullet projectile, Animator animator, NavMeshAgent pathFinder, Transform bossZero, Boss bossObject) : base(bossFSM, projectile, animator, pathFinder)
    {
        bossReference = bossObject;

        bossZeroPoint = bossZero;
        frontAttackPoint = bossZeroPoint.GetChild(0).transform;
        
        // Shooting delay
        delayAttackTime = 2f;

        // Angle to rotate
        angleToRotate = (360 / pointsCount) * Mathf.Deg2Rad;

    }

    public override IEnumerator Enter()
    {
        bossPathFinder.stoppingDistance = 15f;
        return base.Enter();
    }

    public override IEnumerator Attack()
    {
        if (nextAttackTime <= Time.time)
        {
            nextAttackTime = Time.time + 1 / delayAttackTime;
            CircleAttack();
        }
        return base.Attack();
    }

    private void CircleAttack()
    {
        // Apply passive damage to boss
        bossReference.applyDamage(20);

        // Local position of attack point relatively of boss zero point
        Vector3 localAttackPoint = frontAttackPoint.position - bossZeroPoint.transform.position;

        // Store first attack points vector
        Vector3 firstAttackPoint = localAttackPoint;

        // Calculate angle relatively boss rotation
        float angle = Vector3.SignedAngle(Vector3.forward, firstAttackPoint, Vector3.up);

        // Set rotation for first bullet on orbit
        Quaternion newRot = Quaternion.Euler(0, angle, 0);

        // For each point we need to create
        for (int i = 0; i < pointsCount; i++)
        {
            // Create new object
            UnityEngine.Object.Instantiate(bossProjectile, bossZeroPoint.transform.position + localAttackPoint, newRot);

            // Calc new bullet transform on orbit depending on previous value of x and z
            float X_pos = Mathf.Cos(angleToRotate) * localAttackPoint.x - Mathf.Sin(angleToRotate) * localAttackPoint.z;
            float Z_pos = Mathf.Sin(angleToRotate) * localAttackPoint.x + Mathf.Cos(angleToRotate) * localAttackPoint.z;

            // Save new values of orbit
            localAttackPoint = new Vector3(X_pos, localAttackPoint.y, Z_pos);

            // Calc new rotation for each bullet on orbit (angle - relative to boss object)
            newRot = Quaternion.Euler(0, angle + Vector3.SignedAngle(firstAttackPoint, localAttackPoint, Vector3.up), 0);
        }
    }
}

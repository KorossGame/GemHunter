using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : BossState
{
    private Transform bossZeroPoint;

    private float pointsCount = 30;
    private float radius = 5f;

    public Rage(BossFSM bossFSM, BossBullet projectile, Animator animator, Transform bossZero) : base(bossFSM, projectile, animator)
    {
        bossZeroPoint = bossZero;
        delayAttackTime = 0.5f;
    }

    public override IEnumerator Enter()
    {
        Debug.Log("Sorcerer");
        return base.Enter();
    }

    public override IEnumerator Attack()
    {
        if (nextAttackTime <= Time.time)
        {
            nextAttackTime = Time.time + delayAttackTime;
            CircleAttack();
        }
        return base.Attack();
    }

    public void CircleAttack()
    {
        Vector3 initialPoint = Vector3.forward;
        Vector3 newPos = initialPoint;
        Quaternion newRot = new Quaternion(0, 0, 0, 0);

        // Angle to rotate
        float angleToRotate = (360 / pointsCount) * Mathf.Deg2Rad;
        
        // For each point we need to create
        for (int i = 0; i < pointsCount; i++)
        {
            BossBullet ob = UnityEngine.Object.Instantiate(bossProjectile, bossZeroPoint.transform.position + newPos * radius, newRot);

            // Calc new bullet transform on orbit depending on previous value of variable
            float X_pos = Mathf.Cos(angleToRotate) * newPos.x - Mathf.Sin(angleToRotate) * newPos.z;
            float Z_pos = Mathf.Sin(angleToRotate) * newPos.x + Mathf.Cos(angleToRotate) * newPos.z;

            // New values of orbit
            newPos = new Vector3(X_pos, initialPoint.y, Z_pos);

            // Calc new rotation for each bullet on orbit
            newRot = Quaternion.Euler(0, Vector3.SignedAngle(initialPoint, newPos, Vector3.up), 0);
        }
    }
}

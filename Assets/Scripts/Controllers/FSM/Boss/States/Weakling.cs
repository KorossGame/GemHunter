using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weakling : BossState
{
    private Transform bossHolderPoint;

    public Weakling(BossFSM bossFSM, BossBullet projectile, Animator animator, Transform holderPoint) : base(bossFSM, projectile, animator)
    {
        bossHolderPoint = holderPoint;
    }

    public override IEnumerator Enter()
    {
        Debug.Log("Weakling");
        return base.Enter();
    }

    public override IEnumerator Attack()
    {
        if (nextAttackTime <= Time.time)
        {
            nextAttackTime = Time.time + 1 / delayAttackTime;

            // Spawn bullet
            UnityEngine.Object.Instantiate(bossProjectile, bossHolderPoint.position, bossHolderPoint.rotation);
        }
        return base.Attack();
    }
}

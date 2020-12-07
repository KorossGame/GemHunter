using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Weakling : BossState
{
    private Transform bossHolderPoint;

    public Weakling(BossFSM bossFSM, BossBullet projectile, Animator animator, NavMeshAgent pathFinder, Transform holderPoint) : base(bossFSM, projectile, animator, pathFinder)
    {
        bossHolderPoint = holderPoint;
    }

    public override IEnumerator Enter()
    {
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

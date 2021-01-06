using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sorcerer : BossState
{
    private Transform starAttackPoints;

    public Sorcerer(BossFSM bossFSM, BossBullet projectile, Animator animator, NavMeshAgent pathFinder, Transform starAttackPointParent) : base(bossFSM, projectile, animator, pathFinder)
    {
        starAttackPoints = starAttackPointParent;
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

            // For each child spawn enemy bullet
            foreach (Transform child in starAttackPoints)
            {
                UnityEngine.Object.Instantiate(bossProjectile, child.position, child.rotation);
            }
        }
        return base.Attack();
    }
}

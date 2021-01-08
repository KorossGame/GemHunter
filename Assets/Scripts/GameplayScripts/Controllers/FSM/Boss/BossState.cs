using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BossState : State
{
    [Header("Attack")]
    protected float nextAttackTime;
    protected float delayAttackTime = 1f;

    [Header("Access to boss")]
    protected Animator bossAnimator;
    protected BossFSM bossFSMObject;
    protected BossBullet bossProjectile;
    protected NavMeshAgent bossPathFinder;

    // Each State have access to boss FSM (to change states), projectile, animator and enemy AI
    public BossState(BossFSM bossFSM, BossBullet projectile, Animator animator, NavMeshAgent pathFinder)
    {
        bossAnimator = animator;
        bossFSMObject = bossFSM;
        bossProjectile = projectile;
        bossPathFinder = pathFinder;
    }

    public virtual IEnumerator Attack()
    {
        yield break;
    }

    protected virtual void ChangeStopDistance()
    {
        // in different phases we want different stop distance for boss
    }
}

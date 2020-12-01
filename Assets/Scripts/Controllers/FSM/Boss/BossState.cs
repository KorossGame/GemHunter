using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState : State
{
    [Header("Attack")]
    protected float nextAttackTime;
    protected float delayAttackTime = 1f;

    [Header("Access to boss")]
    protected Animator bossAnimator;
    protected BossFSM bossFSMObject;
    protected BossBullet bossProjectile;

    // Each State have access to boss FSM (to change states), state projectile and animator
    public BossState(BossFSM bossFSM, BossBullet projectile, Animator animator)
    {
        bossAnimator = animator;
        bossFSMObject = bossFSM;
        bossProjectile = projectile;
    }

    public virtual IEnumerator Attack()
    {
        yield break;
    }

    public virtual void applyDamage(int damage)
    {
        // As boss activates godMode, we need to check if damage wouldn't be letal
    }

    protected virtual void ChangeStopDistance()
    {
        // in different phases we want different stop distance for boss
    }
}

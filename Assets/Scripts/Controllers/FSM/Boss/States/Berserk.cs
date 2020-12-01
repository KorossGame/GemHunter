using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserk : BossState
{
    public Berserk(BossFSM bossFSM, BossBullet projectile, Animator animator) : base(bossFSM, projectile, animator)
    {
    }

    public override IEnumerator Enter()
    {
        Debug.Log("Berserk");
        return base.Enter();
    }

    public override IEnumerator Attack()
    {
        if (nextAttackTime <= Time.time)
        {
            nextAttackTime = Time.time + 1 / delayAttackTime;
        }
        return base.Attack();
    }
}

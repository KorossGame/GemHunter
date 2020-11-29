using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weakling : BossState
{
    public Weakling(BossFSM bossFSM, Boss boss) : base(bossFSM, boss)
    {
        delayAttackTime = 1f;
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
            Instantiate(bossObject.attackProjectile, bossObject.holderPoint.position, bossObject.holderPoint.rotation);
        }
        return base.Attack();
    }
}

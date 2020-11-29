using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState : State
{
    [Header("Attack")]
    protected float nextAttackTime;
    protected float delayAttackTime;

    [Header("FSM")]
    //protected Animator bossAnimator;
    protected BossFSM bossFSMObject;
    protected Boss bossObject;

    // Each State have access to boss FSM and animator
    public BossState(BossFSM bossFSM, Boss boss)
    {
        bossFSMObject = bossFSM;
        bossObject = boss;
        //bossAnimator = animator;
    }

    public virtual IEnumerator Attack()
    {
        yield break;
    }


}

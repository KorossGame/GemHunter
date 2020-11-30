using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorcerer : BossState
{
    private Transform starAttackPoints;

    public Sorcerer(BossFSM bossFSM, BossBullet projectile, Animator animator, Transform starAttackPointParent) : base(bossFSM, projectile, animator)
    {
        starAttackPoints = starAttackPointParent;
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
            nextAttackTime = Time.time + 1 / delayAttackTime;

            // For each child spawn enemy bullet
            foreach (Transform child in starAttackPoints)
            {
                GameObject.Instantiate(bossProjectile, child.position, child.rotation);
            }
        }
        return base.Attack();
    }
}

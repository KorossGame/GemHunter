using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class God : BossState
{
    private Boss bossObject;
    private GameObject energeticFieldObject;

    public God (BossFSM bossFSM, BossBullet projectile, Animator animator, NavMeshAgent pathFinder, Boss bossReference, GameObject energeticField) : base(bossFSM, projectile, animator, pathFinder)
    {
        bossObject = bossReference;
        energeticFieldObject = energeticField;
    }

    public override IEnumerator Enter()
    {
        if (!bossObject.godActivated)
        {
            bossObject.godActivated = true;
            bossPathFinder.stoppingDistance = 200f;
            TeleportToCenterAndTurnGravityOff();
            bossObject.RegenMaxHP();
            SpawnTurrets();
        }
        return base.Enter();
    }

    public override IEnumerator Attack()
    {
        return base.Attack();
    }

    private void TeleportToCenterAndTurnGravityOff()
    {
        bossObject.GetComponent<Rigidbody>().useGravity = false;
        bossObject.transform.position = new Vector3(0, 2.5f, 0);
        bossPathFinder.baseOffset = 0.75f;
    }

    private void SpawnTurrets()
    {
        UnityEngine.Object.Instantiate(energeticFieldObject, new Vector3(0, 1.5f, 0), Quaternion.identity);
    }
}

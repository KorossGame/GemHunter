using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Enemy
{
    [Header("Stats")]
    private float maxHP;

    [Header("FSM")]
    private BossFSM stateMachine;

    [Header("Animator")]
    private Animator animator;

    [Header("Attack projectiles for different states")]
    public BossBullet[] attackProjectiles;

    [Header("Attack types")]
    public Transform starAttackPointParent;

    void Awake()
    {
        // HP and Speed of movement
        HP = 2500;
        maxHP = HP;
        Speed = 5f;
    }

    private void Start()
    {
        // Get player reference
        player = PlayerManager.instance.player.transform;

        // Set AI movement speed
        pathFinder = GetComponent<NavMeshAgent>();
        pathFinder.speed = Speed;

        // Set weapon holder
        holderPoint = transform.Find("HolderPoint");

        // Start infinite loop of finding path to player
        StartCoroutine(UpdatePath());

        // Activate FSM
        stateMachine = GetComponent<BossFSM>();
        stateMachine.changeState(new Rage(stateMachine, attackProjectiles[0], animator, starAttackPointParent));
        //stateMachine.changeState(new Sorcerer(stateMachine, attackProjectiles[0], animator, starAttackPointParent));
    }

    private void Update()
    {
        FaceTarget();
        StartCoroutine(stateMachine.currentBossState.Attack());
    }

    public override void applyDamage(int damage)
    {
        stateMachine.currentBossState.applyDamage(damage);
    }

    protected override void Die()
    {
        //stateMachine.changeState(new Dead(stateMachine, this));
        base.Die();
    }
}

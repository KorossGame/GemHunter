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

    [HideInInspector]
    private float stoppingDistance = 100f;

    [Header("Attack projectiles for different states")]
    public BossBullet attackProjectile;

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
        stateMachine.changeState(new Weakling(stateMachine, this));
    }

    private void Update()
    {
        FaceTarget();
        StartCoroutine(stateMachine.currentState.Attack());
    }

    public override void applyDamage(int damage)
    {
        // As boss activates godMode, we need to check if damage wouldn't be letal
        if (HP - damage > 0)
        {
            base.applyDamage(damage);
        }
    }

    protected override void Die()
    {
        //stateMachine.changeState(new Dead(stateMachine, this));
        base.Die();
    }

    protected override void ChangeStopDistance()
    {
        pathFinder.stoppingDistance = stoppingDistance;
    }
}

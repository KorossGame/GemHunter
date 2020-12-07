using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Enemy
{
    [Header("Stats")]
    private int maxHP;

    [Header("FSM")]
    private BossFSM stateMachine;

    [Header("Animator")]
    private Animator animator;

    [Header("Attack projectiles for different states")]
    public BossBullet[] attackProjectiles;

    [Header("Attack types")]
    public Transform[] attackPoints;

    // Object for last phase
    public GameObject energeticField;

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
        stateMachine.changeState(new God(stateMachine, attackProjectiles[1], animator, pathFinder, this, energeticField));
        //stateMachine.changeState(new Nightmare(stateMachine, attackProjectiles[1], animator, pathFinder, player.transform));
        //stateMachine.changeState(new Berserk(stateMachine, attackProjectiles[0], animator, pathFinder, attackPoints[1]));
        //stateMachine.changeState(new Rage(stateMachine, attackProjectiles[0], animator, pathFinder, attackPoints[1]));
        //stateMachine.changeState(new Sorcerer(stateMachine, attackProjectiles[0], animator, pathFinder, attackPoints[1]));
        //stateMachine.changeState(new Weakling(stateMachine, attackProjectiles[0], animator, pathFinder, attackPoints[0]));
    }

    private void Update()
    {
        FaceTarget();
        if (stateMachine.currentBossState != null)
        {
            StartCoroutine(stateMachine.currentBossState.Attack());
        }
    }

    public void RegenMaxHP()
    {
        HP = maxHP;
    }

    protected override void Die()
    {
        base.Die();
    }
}

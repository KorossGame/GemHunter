using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Enemy
{
    [Header("Stats")]
    private int maxHP;
    public bool godActivated = false;

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

    void OnEnable()
    {
        StartCoroutine(Spawner.instance.KillAllEnemies());
    }

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

        // Add all possible states to stateMachine
        stateMachine.AddState(new Weakling(stateMachine, attackProjectiles[0], animator, pathFinder, attackPoints[0]));
        stateMachine.AddState(new Sorcerer(stateMachine, attackProjectiles[0], animator, pathFinder, attackPoints[1]));
        stateMachine.AddState(new Rage(stateMachine, attackProjectiles[0], animator, pathFinder, attackPoints[1]));
        stateMachine.AddState(new Berserk(stateMachine, attackProjectiles[0], animator, pathFinder, attackPoints[1]));
        stateMachine.AddState(new Nightmare(stateMachine, attackProjectiles[1], animator, pathFinder, player.transform));
        stateMachine.AddState(new God(stateMachine, attackProjectiles[1], animator, pathFinder, this, energeticField));

        // Switch to first state
        stateMachine.changeState(stateMachine.possibleStates[0]);
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

    public override void applyDamage(int damage)
    {
        /*
         * 100%: 2001-2500
         * 80%:  1501-2000
         * 60%:  1001-1500
         * 40%:  501-1000
         * 20%:  126-500
         * 5%:   0-125
         */
        
        if (stateMachine.currentBossState == stateMachine.possibleStates[4] && HP - damage <= 0)
        {
            damage = HP - 10;
        }

        HP -= damage;

        if (HP >= 2001)
        {
            stateMachine.changeState(stateMachine.possibleStates[0]);
        }
        else if (HP >= 1501)
        {
            stateMachine.changeState(stateMachine.possibleStates[1]);
        }
        else if (HP >= 1001)
        {
            stateMachine.changeState(stateMachine.possibleStates[2]);
        }
        else if (HP >= 501)
        {
            stateMachine.changeState(stateMachine.possibleStates[3]);
        }
        else if (HP >= 126)
        {
            stateMachine.changeState(stateMachine.possibleStates[4]);
        }
        else if (HP > 0)
        {
            stateMachine.changeState(stateMachine.possibleStates[5]);
        }
        else
        {
            Die();
        }
    }

    protected override void Die()
    {
        base.Die();
    }
}

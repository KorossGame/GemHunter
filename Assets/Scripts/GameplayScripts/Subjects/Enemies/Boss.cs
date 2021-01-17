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

    [Header("Attack projectiles for different states")]
    public BossBullet[] attackProjectiles;

    [Header("Attack types")]
    public Transform[] attackPoints;

    // Object for last phase
    public GameObject energeticField;

    private Coroutine c;

    private void Awake()
    {
        // HP and Speed of movement
        HP = 2500;
        maxHP = HP;
        Speed = 5f;
    }

    private void Start()
    {
        if (HPBar)
        {
            HPBar.fillAmount = HP * 1.0f / maxHP * 1.0f;
        }

        // Get player reference
        player = PlayerManager.instance.player.transform;

        // Set AI movement speed
        pathFinder = GetComponent<NavMeshAgent>();
        pathFinder.speed = Speed;

        // Set weapon holder
        holderPoint = transform.Find("HolderPoint");

        // Activate FSM
        stateMachine = GetComponent<BossFSM>();

        // Add all possible states to stateMachine
        stateMachine.AddState(new Weakling(stateMachine, attackProjectiles[0], animator, pathFinder, attackPoints[0]));
        stateMachine.AddState(new Sorcerer(stateMachine, attackProjectiles[0], animator, pathFinder, attackPoints[1]));
        stateMachine.AddState(new Rage(stateMachine, attackProjectiles[0], animator, pathFinder, attackPoints[1], this));
        stateMachine.AddState(new Berserk(stateMachine, attackProjectiles[0], animator, pathFinder, attackPoints[1]));
        stateMachine.AddState(new Nightmare(stateMachine, attackProjectiles[1], animator, pathFinder, player.transform));
        stateMachine.AddState(new God(stateMachine, attackProjectiles[1], animator, pathFinder, this, energeticField));

        // Switch to first state
        stateMachine.changeState(stateMachine.possibleStates[0]);

        // Wait for cutscene to play
        activated = false;
    }

    private void Update()
    {
        if (activated)
        {
            if (c == null)
            {
                // Start loop of finding path to player
                c = StartCoroutine(UpdatePath());
            }
            
            FaceTarget();

            if (stateMachine.currentBossState != null && activated)
            {
                if (pathFinder.remainingDistance <= pathFinder.stoppingDistance)
                {
                    StartCoroutine(stateMachine.currentBossState.Attack());
                }
            }
        }
    }

    public void RegenMaxHP()
    {
        HP = maxHP / 3;
    }

    public override void applyDamage(int damage)
    {
        // Play sound being hit
        AudioManager.instance.PlaySound("Hit");

        // If not last phase but got fatal damage reduce damage
        if (stateMachine.currentBossState == stateMachine.possibleStates[4] && HP - damage <= 0)
        {
            damage = HP - 10;
        }

        // Substract hp
        HP -= damage;

        // Set new phase based on HP
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

        // Update UI
        if (HPBar)
        {
            HPBar.fillAmount = HP * 1.0f / maxHP * 1.0f;
        }
    }

    protected override void Die()
    {
        AudioManager.instance.PlaySound("BossDie");
        base.Die();
    }
}

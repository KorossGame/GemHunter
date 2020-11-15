﻿using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

abstract public class Enemy : Subject
{
    // PowerUP spawn
    private float powerUPdropChance = 10f;

    // Pathfinder
    private NavMeshAgent pathFinder;
    protected Transform player;
    private bool dead = false;

    // Enemy attack
    protected bool hasMelee;
    protected float attackDistance;
    protected Gun currentWeapon;

    // Enemy dead event
    public event System.Action OnDeath;

    // Chances of spawn enemy with particular weapon (e.g. 10 for melee - represents values (0-10])
    protected double meleeChance;
    protected double pistolChance;
    protected double shotgunChance;
    protected double assaultChance;
    protected double RPGChance;

    // Weapon holder point
    public Transform holderPoint;

    // Object of enemy
    public Subject enemyObject;

    void Start()
    {
        // Set random seed
        Random.InitState((int)System.DateTime.Now.Ticks);

        // Get object itself
        enemyObject = gameObject.GetComponent<Subject>();

        // Get player reference
        player = PlayerManager.instance.player.transform;

        // Set AI movement speed
        pathFinder = gameObject.GetComponent<NavMeshAgent>();
        pathFinder.speed = Speed;

        // Set weapon holder
        holderPoint = transform.Find("HolderPoint");

        // Get Weapon for Enemy
        getWeapon();

        // Start infinite loop of finding path to player
        StartCoroutine(UpdatePath());
    }

    void Update()
    {
        if (player)
        {
            if (pathFinder.remainingDistance <= pathFinder.stoppingDistance)
            {
                currentWeapon.Shoot(enemyObject);
            }
        }
    }

    void FixedUpdate()
    {
        FaceTarget();
    }

    protected void getWeapon()
    {
        // Get random number
        float gunRandom = Random.Range(0f, 100.0f);

        // Set current weapon
        if (gunRandom <= meleeChance)
        {
            currentWeapon = GunManager.instance.guns[0];
        }
        else if (gunRandom <= pistolChance)
        {
            currentWeapon = GunManager.instance.guns[1];
        }
        else if (gunRandom <= shotgunChance)
        {
            currentWeapon = GunManager.instance.guns[2];
        }
        else if (gunRandom <= assaultChance)
        {
            currentWeapon = GunManager.instance.guns[3];
        }
        else if (gunRandom <= RPGChance)
        {
            currentWeapon = GunManager.instance.guns[4];
        }

        // Create a new gun for enemy in holder point position
        currentWeapon = Instantiate(currentWeapon, holderPoint.transform.position, gameObject.transform.rotation, holderPoint.transform);

        // Change Nav Mesh agent stopping distance depending on gun got
        ChangeStopDistance();
    }

    IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f;
        while (player && !dead)
        {
            Vector3 PlayerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            pathFinder.SetDestination(PlayerPos);
            yield return new WaitForSeconds(refreshRate);
        }
        if (!player)
        {
            pathFinder.SetDestination(transform.position);
        }
    }

    protected void FaceTarget()
    {
        if (player)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion checkRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, checkRotation, Time.fixedDeltaTime * pathFinder.angularSpeed);
        }
    }

    protected override void Die()
    {
        dead = true;
        // Animation and Sound of death

        // Generate power up
        GeneratePowerUP();

        // Spawner interaction event
        OnDeath?.Invoke();


        // Destroy GameObject
        base.Die();
    }
    public override void applyDamage(int damage)
    {
        // Play custom animation and sound

        // Calc damage
        base.applyDamage(damage);
    }

    protected void GeneratePowerUP()
    {
        // Generate new number to define if generate powerUP is needed
        int randomNumber = (int)Random.Range(0, 100);

        print(randomNumber);

        if (randomNumber <= powerUPdropChance)
        {
            // Which powerUP going to be dropped
            int randomPowerUP = Random.Range(0, PowerUPManager.instance.powerUPs.Length - 1);

            // Create PowerUP object
            Instantiate(PowerUPManager.instance.powerUPs[randomPowerUP], transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
        }
    }


    // Change Nav Mesh agent stopping distance depending on gun got
    protected void ChangeStopDistance()
    {
        pathFinder.stoppingDistance = currentWeapon.EffectiveRange * 1.5f;
    }
}

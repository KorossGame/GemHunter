using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

abstract public class Enemy : Subject
{
    // PowerUP spawn
    private int powerUPcount = 3;
    private float powerUPdropChange = 10f;
    private float maxValue = 100;

    // Pathfinder
    private NavMeshAgent pathFinder;
    protected Transform player;
    private bool notRunned = true;
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

    protected void getWeapon()
    {
        // Set random seed
        Random.InitState((int)System.DateTime.Now.Ticks);

        // Get random number
        float gunRandom = Random.Range(0f, 100.0f);

        // Set current weapon
        if (gunRandom <= meleeChance)
        {
            currentWeapon = GunManager.instance.meleeWeapon;
        }
        else if (gunRandom <= pistolChance)
        {
            currentWeapon = GunManager.instance.pistolWeapon;
        }
        else if (gunRandom <= shotgunChance)
        {
            currentWeapon = GunManager.instance.shotgunWeapon;
        }
        else if (gunRandom <= assaultChance)
        {
            currentWeapon = GunManager.instance.assaultWeapon;
        }
        else if (gunRandom <= RPGChance)
        {
            currentWeapon = GunManager.instance.RPGWeapon;
        }

        // Create a new gun for enemy in holder point position
        Instantiate(currentWeapon, holderPoint.transform.position, gameObject.transform.rotation, holderPoint.transform);
        
        // Enemies have infinite ammo
        currentWeapon.MaxAmmo = -1;

        // Change Nav Mesh agent stopping distance depending on gun got
        ChangeStopDistance();
    }

    void Update()
    {
        // BAD SOLUTION
        if (!pathFinder)
        {
            pathFinder = GetComponent<NavMeshAgent>();
            pathFinder.speed = Speed;
        }
        if (!player)
        {
            player = PlayerManager.instance.player.transform;
        }
        if (player && pathFinder && notRunned)
        {
            StartCoroutine(UpdatePath());
            notRunned = false;
        }
        if (!currentWeapon)
        {
            getWeapon();
        }
        if (!holderPoint)
        {
            holderPoint = transform.Find("HolderPoint");
        }

        if (pathFinder.remainingDistance <= pathFinder.stoppingDistance)
        {
            currentWeapon.Shoot();
        }
    }

    IEnumerator UpdatePath()
    {
        float refreshRate = 0.5f;
        while (player && !dead)
        {
            Vector3 PlayerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            pathFinder.SetDestination(PlayerPos);
            FaceTarget();
            yield return new WaitForSeconds(refreshRate);
        }
    }

    protected void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion checkRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, checkRotation, Time.deltaTime * 10f);
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
        // Set random seed
        Random.InitState((int)System.DateTime.Now.Ticks);
        
        // Generate new number to define if generate powerUP is needed
        int randomNumber = (int)Random.Range(0, maxValue);

        // Calc if we got value in drop chance
        if (randomNumber > maxValue * powerUPdropChange) return;

        // Which powerUP going to be dropped
        int randomPowerUP = Random.Range(0, powerUPcount);
        //Instantiate(powerUP, transform.position, transform.rotation);
    }


    // Change Nav Mesh agent stopping distance depending on gun got
    protected void ChangeStopDistance()
    {
        pathFinder.stoppingDistance = currentWeapon.EffectiveRange;
    }
}

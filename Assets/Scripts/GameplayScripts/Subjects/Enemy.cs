using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

abstract public class Enemy : Subject
{
    protected int initialHP;
    public Image HPBar;

    [SerializeField] private ParticleSystem spawnParticles;
    [SerializeField] private ParticleSystem dieParticles;

    public float powerUPdropChance { get; set; } = 10f;
    public float ammoBoxDropChance { get; set; } = 40f;

    [Header("Pathfinder")]
    protected NavMeshAgent pathFinder;
    protected Transform player;
    private bool dead = false;

    [Header("Enemy Attack")]
    protected bool hasMelee;
    protected float attackDistance;
    protected Gun currentWeapon;
    
    [HideInInspector]
    public event Action OnDeath;

    [Header("Weapon")]
    // Chances of spawn enemy with particular weapon (e.g. 10 for melee - represents values (0-10])
    protected double[] weaponChance;

    // Weapon holder point
    public Transform holderPoint;

    [HideInInspector]
    // Object of enemy
    private Subject enemyObject;

    private void OnDisable()
    {
        OnDeath = null;
    }

    private void Start()
    {
        // Spawn particles
        spawnParticles.Play();

        // Set HP and update HP bar
        initialHP = HP;
        if (HPBar)
        {
            HPBar.fillAmount = HP * 1.0f / initialHP * 1.0f;
        }

        animator = GetComponent<Animator>();

        // Set random seed
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);

        // Weapon chance array
        generatePartialSum();

        // Get object itself
        enemyObject = GetComponent<Subject>();

        // Set AI movement speed
        pathFinder = GetComponent<NavMeshAgent>();
        pathFinder.speed = Speed;

        // Set weapon holder
        holderPoint = transform.Find("HolderPoint");

        // Get Weapon for Enemy
        getWeapon();

        // Get player reference
        findPlayerReference();

        // Start infinite loop of finding path to player
        StartCoroutine(UpdatePath());
    }

    private void Update()
    {
        // If player still is not set up - try to find it
        if (!player)
        {
            findPlayerReference();
            return;
        }

        // Shoot player
        if (player && currentWeapon && activated)
        {
            if (pathFinder.remainingDistance <= pathFinder.stoppingDistance)
            {
                currentWeapon.Shoot(enemyObject);
            }
        }
    }

    protected void FixedUpdate()
    {
        FaceTarget();
    }

    private void findPlayerReference()
    {
        try
        {
            if (PlayerManager.instance && PlayerManager.instance.player)
            {
                player = PlayerManager.instance.player.transform;
            }
        }
        catch (UnassignedReferenceException)
        {
            player = null;
        }
    }

    protected virtual void getWeapon()
    {
        if (weaponChance != null)
        {
            // Get random number
            float gunRandom = UnityEngine.Random.Range(0f, 100.0f);

            // Set current weapon
            if (gunRandom <= weaponChance[0])
            {
                currentWeapon = GunManager.instance.guns[0];
            }
            else if (gunRandom <= weaponChance[1])
            {
                currentWeapon = GunManager.instance.guns[1];
            }
            else if (gunRandom <= weaponChance[2])
            {
                currentWeapon = GunManager.instance.guns[2];
            }
            else if (gunRandom <= weaponChance[3])
            {
                currentWeapon = GunManager.instance.guns[3];
            }
            else if (gunRandom <= weaponChance[4])
            {
                currentWeapon = GunManager.instance.guns[4];
            }

            // Create a new gun for enemy in holder point position
            currentWeapon = Instantiate(currentWeapon, holderPoint.transform.position, gameObject.transform.rotation, holderPoint.transform);

            // Change Nav Mesh agent stopping distance depending on gun got
            ChangeStopDistance();
        }
    }

    protected IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f;
        if (activated)
        {
            while (player && !dead)
            {
                pathFinder.SetDestination(player.position);
                yield return new WaitForSeconds(refreshRate);
            }

            // If no player or enemy is deactivated - stay on place
            if (!player)
            {
                pathFinder.SetDestination(transform.position);
            }
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
        if (dead) return;

        dead = true;

        // Animation and Sound of death
        AudioManager.instance.PlaySound("DieSound");
        dieParticles.Play();

        // If enemy didn't drop power up we may drop some ammo
        if (!GeneratePowerUP())
        {
            GenerateAmmoBox();
        }

        // Call OnDeath event
        OnDeath?.Invoke();

        if (animator != null)
        {
            ChangeAnimationState("Die");
        }
        
    }

    public override void applyDamage(int damage)
    {
        base.applyDamage(damage);
        
        if (HPBar)
        {
            HPBar.fillAmount = HP * 1.0f / initialHP * 1.0f;
        }
    }

    protected bool GeneratePowerUP()
    {
        // Generate new number to define if generate powerUP is needed
        int randomNumber = (int)UnityEngine.Random.Range(0, 100);

        if (randomNumber <= powerUPdropChance)
        {
            // Which powerUP going to be dropped
            int randomPowerUP = UnityEngine.Random.Range(0, PowerUPManager.instance.powerUPs.Length - 1);

            // Create PowerUP object
            Instantiate(PowerUPManager.instance.powerUPs[randomPowerUP], transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
            return true;
        }
        return false;
    }

    protected void GenerateAmmoBox()
    {
        // Generate new number to define if generate an ammo box
        int randomNumber = (int)UnityEngine.Random.Range(0, 100);

        if (randomNumber <= ammoBoxDropChance)
        {
            // Which powerUP going to be dropped
            int randomAmmoBox = UnityEngine.Random.Range(0, AmmoManager.instance.ammoBoxes.Length - 1);

            // Create ammo box object
            Instantiate(AmmoManager.instance.ammoBoxes[randomAmmoBox], transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
        }
    }

    // Change Nav Mesh agent stopping distance depending on gun got
    protected virtual void ChangeStopDistance()
    {
        pathFinder.stoppingDistance = currentWeapon.EffectiveRange * 1.5f;
    }

    protected void generatePartialSum()
    {
        double counter = 0;
        for (byte i = 0; i < weaponChance.Length; i++)
        {
            counter += weaponChance[i];
            weaponChance[i] = counter;
        }
    }
}

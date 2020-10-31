using System.Collections;
using UnityEngine;
using UnityEngine.AI;

abstract public class Enemy : Subject
{
    // PowerUP spawn
    private int powerUPcount = 3;
    private float powerUPdropChange = 0.1f;
    private float maxValue = 1;

    // Pathfinder
    private NavMeshAgent pathFinder;
    protected Transform player;
    private bool notRunned = true;

    void Start()
    {
    }

    void Update()
    {
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
    }

    IEnumerator UpdatePath()
    {
        float refreshRate = 0.5f;
        while (player)
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
        // Animation and Sound of death

        // Generate power up
        GeneratePowerUP();

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
        // Instanciate(powerUP, transform.position, transform.rotation);
    }
}

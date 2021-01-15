using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialGameManager : MonoBehaviour
{
    public static TutorialGameManager instance;
    
    // Spawn points to spawn enemies
    public Transform enemySpawnPoint;
    public Transform powerUPSpawnPoint;
    public Transform ammoBoxSpawnPoint;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SpawnEnemy();
        SpawnPowerUP();
        SpawnAmmoBox();
    }

    private IEnumerator WaitForSomeTime(float time, Action toDo)
    {
        yield return new WaitForSeconds(time);
        toDo();
    }

    public void EnemyPhase()
    {
        StartCoroutine(WaitForSomeTime(1f, SpawnEnemy));
    }

    private void PowerUPPhase()
    {
        StartCoroutine(WaitForSomeTime(2f, SpawnPowerUP));
    }

    private void AmmoBoxPhase()
    {
        StartCoroutine(WaitForSomeTime(1f, SpawnAmmoBox));
    }

    private void SpawnEnemy()
    {
        // Create enemy object
        Enemy e = Instantiate(Spawner.instance.enemyTypes[UnityEngine.Random.Range(0, Spawner.instance.enemyTypes.Length - 1)], enemySpawnPoint.position, Quaternion.identity);

        // Disable any attack
        e.activated = false;

        // Disable any drop
        e.ammoBoxDropChance = -1;
        e.powerUPdropChance = -1;

        // When it dies summon next enemy type
        e.OnDeath += EnemyPhase;
    }

    private void SpawnPowerUP()
    {
        // Create powerUP object
        PowerUP power = Instantiate(PowerUPManager.instance.powerUPs[UnityEngine.Random.Range(0, PowerUPManager.instance.powerUPs.Length)], powerUPSpawnPoint.position, Quaternion.identity);

        // On pickup summon next
        power.OnPickUP += PowerUPPhase;

        // If player doesnt pick up it in time - spawn new
        power.OnDestroy += PowerUPPhase;
    }

    private void SpawnAmmoBox()
    {
        // Spawn each type of ammo boxes
        // Create powerUP object
        AmmoBox box = Instantiate(AmmoManager.instance.ammoBoxes[UnityEngine.Random.Range(0, AmmoManager.instance.ammoBoxes.Length)], ammoBoxSpawnPoint.position, Quaternion.identity);

        // On pickup summon next
        box.OnPickUP += AmmoBoxPhase;

        // If player does not pick up in time - spawn new
        box.OnDestroy += AmmoBoxPhase;

    }
}

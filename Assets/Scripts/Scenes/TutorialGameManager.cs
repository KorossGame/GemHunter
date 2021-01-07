using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialGameManager : MonoBehaviour
{
    public static TutorialGameManager instance;

    [SerializeField] private Transform enemySpawnPoint;
    private byte enemyIntroductionCounter = 0;

    [SerializeField] private Transform powerUPSpawnPoint;
    private byte powerUPCounter = 0;

    [SerializeField] private Transform ammoBoxSpawnPoint;
    private byte ammoBoxCounter = 0;

    private void Awake()
    {
        instance = this;
    }

    private IEnumerator WaitForSomeTime(float time, Action toDo)
    {
        yield return new WaitForSeconds(time);
        toDo();
    }

    public void EnemyPhase()
    {
        StartCoroutine(WaitForSomeTime(2f, SpawnEnemy));
    }

    private void PowerUPPhase()
    {
        StartCoroutine(WaitForSomeTime(2f, SpawnPowerUP));
    }

    private void AmmoBoxPhase()
    {
        StartCoroutine(WaitForSomeTime(1.5f, SpawnAmmoBox));
    }

    private void SpawnEnemy()
    {
        // Check if players met all type of enemies
        if (enemyIntroductionCounter < Spawner.instance.enemyTypes.Length)
        {
            // Create enemy object
            Enemy e = Instantiate(Spawner.instance.enemyTypes[enemyIntroductionCounter], enemySpawnPoint.position, Quaternion.identity);

            // Disable any attack
            e.activated = false;

            // Disable any drop
            e.ammoBoxDropChance = -1;
            e.powerUPdropChance = -1;

            // When it dies summon next enemy type
            e.OnDeath += EnemyPhase;

            // Increment counter
            enemyIntroductionCounter++;
        }
        else
        {
            PowerUPPhase();
        }
    }

    private void SpawnPowerUP()
    {
        // Spawn each type of powerups
        if (powerUPCounter < PowerUPManager.instance.powerUPs.Length)
        {
            // Create powerUP object
            PowerUP power = Instantiate(PowerUPManager.instance.powerUPs[powerUPCounter], powerUPSpawnPoint.position, Quaternion.identity);

            // On pickup summon next
            power.OnPickUP += PowerUPPhase;

            // Increment counter
            powerUPCounter++;
        }
        else
        {
            AmmoBoxPhase();
        }
    }

    private void SpawnAmmoBox()
    {
        // Spawn each type of ammo boxes
        if (ammoBoxCounter < AmmoManager.instance.ammoBoxes.Length)
        {
            // Create powerUP object
            AmmoBox box = Instantiate(AmmoManager.instance.ammoBoxes[ammoBoxCounter], ammoBoxSpawnPoint.position, Quaternion.identity);

            // On pickup summon next
            box.OnPickUP += AmmoBoxPhase;

            // Increment counter
            ammoBoxCounter++;
        }
        else
        {
            SceneManager.LoadScene("Level");
            Spawner.instance.active = true;
        }
    }
}

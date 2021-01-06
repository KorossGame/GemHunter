﻿using System;
using System.Collections;
using UnityEngine;

public class Player : Subject
{
    // Visual Object reference
    public GameObject visualObject;

    // Respawn point
    public Transform spawnPoint;

    // Time till respawn the player
    private float respawnTime = 5f;

    // Flag to check if respawn in process
    public bool respawning = false;

    // Inventory system
    public WeaponSwitcher inventory;

    /* Multipliers better to make as listeners and throw an event when player picks up a powerup (Security reasons) */

    // Damage multiplier value for guns (for power up mechanics)
    public byte GunPowerUPMultiplier { get; set; } = 1;

    // If player can be damaged (for power up mechanics)
    public bool GodMode { get; set; } = false;

    void Start()
    {
        Speed = 10f;
        HP = 100;
        Spawn();
    }

    public override void applyDamage(int damage)
    {
        // Play custom animation and sound
        if (!GodMode)
        {
            // Calc damage
            base.applyDamage(damage);
        }
    }

    protected override void Die()
    {
        // Play custom animation and sound

        if (!respawning)
        {
            // Disable Visual player object and Gun visual object
            visualObject.GetComponent<MeshRenderer>().enabled = false;

            if (inventory.WeaponEquiped)
            {
                inventory.WeaponEquiped.RefillAmmo();
                inventory.gameObject.SetActive(false);
            }

            // Activate game restart
            // Game.instance.stateMachine.changeState(new RestartState(Game.instance.stateMachine));

            // Respawn player
            StartCoroutine(Respawn());
        }
    }

    private void Spawn()
    {
        // Teleport player to the spawn point
        if (spawnPoint)
        {
            gameObject.transform.position = spawnPoint.position;
        }
        else
        {
            gameObject.transform.position = new Vector3(0, 0.5f, 0);
        }

        // Set spawner status back to active
        if (Spawner.instance != null) Spawner.instance.active = true;

        // Enable Visual player object
        visualObject.GetComponent<MeshRenderer>().enabled = true;

        // Enable gun visual
        if (inventory.WeaponEquiped)
        {
            inventory.gameObject.SetActive(true);
        }
    }

    private IEnumerator Respawn()
    {
        // Set respawn process to True
        respawning = true;

        // Wait for some time
        yield return new WaitForSeconds(respawnTime);

        // Spawn
        Spawn();

        // Set respawning process to false
        respawning = false;
    }
}
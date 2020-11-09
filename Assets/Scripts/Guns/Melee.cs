using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Melee : Gun
{
    // LayerMask for specific layers (10 stands for enemy, 9 for player)
    private LayerMask enemyLayers;

    void Awake()
    {
        // Damage
        DamagePerBullet = 5;

        // Ammo
        CurrentAmmo = -1;

        // Range
        EffectiveRange = 1f;
        maxRange = 1f;

        // Reload
        fireRate = 2f;
    }

    protected override void ShootBullet(Subject shooter)
    {
        int powerUPMultiplier;
        // Check if power shooter is player or enemy
        if (shooter.transform.tag == "Player")
        {
            powerUPMultiplier = PlayerManager.instance.player.GetComponent<Player>().GunPowerUPMultiplier;
            enemyLayers = 1 << 10;
        }
        else
        {
            powerUPMultiplier = 1;
            enemyLayers = 1 << 9;
        }

        // Detect all objects in sphere for specific layers
        Collider[] hit = Physics.OverlapSphere(attackPoint.position, EffectiveRange, enemyLayers);

        // Apply damage to all objects
        foreach (Collider enemy in hit)
        {
            Subject target = enemy.transform.GetComponent<Subject>();

            // Apply damage
            if (target)
            {
                target.applyDamage(DamagePerBullet * powerUPMultiplier);
            }
        }
    }
}

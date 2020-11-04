using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Melee : Gun
{
    void Start()
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

    protected override void ShootBullet()
    {
        // Check if power up is activated
        int powerUPMultiplier = PlayerManager.instance.player.GetComponent<Player>().GunPowerUPMultiplier;

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

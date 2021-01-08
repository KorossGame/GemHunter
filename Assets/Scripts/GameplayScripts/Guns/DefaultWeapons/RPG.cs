using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : Gun
{
    private string explosionSound;

    void Awake()
    {
        // ID of gun
        ID = 4;

        // Damage
        DamagePerBullet = 350;

        // Bullet speed
        BulletSpeed = 15;

        // Ammo
        ammoInClip = 1;
        MaxAmmo = 2;
        ammoLeft = MaxAmmo;
        CurrentAmmo = ammoInClip;

        // Range
        EffectiveRange = 1000f;
        maxRange = 1000f;

        // Reload
        reloadTime = 5f;

        // Fire Rate (1/fireRate)
        fireRate = 1f;

        // Sounds
        shootSound = "RPGShootingSound";
        reloadSound = "RPGReloadSound";
        explosionSound = "RPGExplosion";
    }

    private void applyShereDamage()
    {
        //
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assault : Gun
{
    void Awake()
    {
        // ID of gun
        ID = 3;

        // Damage
        DamagePerBullet = 45;

        // Bullet speed
        BulletSpeed = 40;

        // Ammo
        ammoInClip = 30;
        MaxAmmo = 120;
        ammoLeft = MaxAmmo;
        CurrentAmmo = ammoInClip;

        // Range
        EffectiveRange = 7f;
        maxRange = 25f;

        // Reload
        reloadTime = 1f;

        // Fire Rate (1/fireRate)
        fireRate = 4f;

        // Sounds
        shootSound = "AssaultShootingSound";
        reloadSound = "AssaultReloadSound";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : Gun
{
    void Start()
    {
        // Damage
        DamagePerBullet = 500;

        // Bullet speed
        BulletSpeed = 15;

        // Ammo
        ammoInClip = 1;
        maxAmmo = 2;
        ammoLeft = maxAmmo;
        CurrentAmmo = ammoInClip;

        // Range
        EffectiveRange = 1000f;
        maxRange = 1000f;

        // Reload
        reloadTime = 5f;

        // Fire Rate (1/fireRate)
        fireRate = 1f;
    }
}

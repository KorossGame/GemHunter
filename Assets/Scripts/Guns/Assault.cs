using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assault : Gun
{
    void Awake()
    {
        // Damage
        DamagePerBullet = 30;

        // Bullet speed
        BulletSpeed = 40;

        // Ammo
        ammoInClip = 30;
        MaxAmmo = 90;
        ammoLeft = MaxAmmo;
        CurrentAmmo = ammoInClip;

        // Range
        EffectiveRange = 7f;
        maxRange = 25f;

        // Reload
        reloadTime = 3.5f;

        // Fire Rate (1/fireRate)
        fireRate = 13.5f;
    }
}

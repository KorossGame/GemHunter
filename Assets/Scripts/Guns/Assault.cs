using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assault : Gun
{
    void Start()
    {
        // Damage
        DamagePerBullet = 30;

        // Bullet speed
        BulletSpeed = 40;

        // Ammo
        ammoInClip = 30;
        maxAmmo = 90;
        ammoLeft = maxAmmo;
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

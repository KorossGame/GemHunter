using UnityEngine;

public class Pistol : Gun
{
    void Start()
    {
        // Damage
        DamagePerShot = 15;

        // Bullet speed
        BulletSpeed = 25;

        // Ammo
        ammoInClip = 17;
        maxAmmo = 102;
        ammoLeft = maxAmmo;
        CurrentAmmo = ammoInClip;

        // Range
        EffectiveRange = 10f;
        maxRange = 25f;

        // Reload
        reloadTime = 3f;

        // Fire Rate (1/fireRate)
        fireRate = 4f;
    }
}

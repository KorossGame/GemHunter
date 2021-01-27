using UnityEngine;

public class Pistol : Gun
{
    void Awake()
    {
        // ID of gun
        ID = 1;

        // Damage
        DamagePerBullet = 20;

        // Bullet speed
        BulletSpeed = 25;

        // Ammo
        ammoInClip = 17;
        MaxAmmo = 102;
        ammoLeft = MaxAmmo;
        CurrentAmmo = ammoInClip;

        // Range
        EffectiveRange = 10f;
        maxRange = 25f;

        // Reload
        reloadTime = 0.75f;

        // Fire Rate (1/fireRate) - bigger value -> faster
        fireRate = 5f;

        // Sounds
        shootSound = "PistolShootingSound";
        reloadSound = "PistolReloadSound";
    }
}

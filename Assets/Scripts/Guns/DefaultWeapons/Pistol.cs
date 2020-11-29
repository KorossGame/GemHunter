using UnityEngine;

public class Pistol : Gun
{
    void Awake()
    {
        // ID of gun
        ID = 1;

        // Damage
        DamagePerBullet = 15;

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
        reloadTime = 3f;

        // Fire Rate (1/fireRate)
        fireRate = 4f;
    }
}

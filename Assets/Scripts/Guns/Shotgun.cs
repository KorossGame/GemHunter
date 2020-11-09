using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    private int bulletShootCount = 5;
    private float angle = 15f;

    void Awake()
    {
        // Damage
        DamagePerBullet = 24;

        // Bullet speed
        BulletSpeed = 35;

        // Ammo
        ammoInClip = 5;
        MaxAmmo = 25;
        ammoLeft = MaxAmmo;
        CurrentAmmo = ammoInClip;

        // Range
        EffectiveRange = 5f;
        maxRange = 25f;

        // Reload
        reloadTime = 2.5f;

        // Fire Rate (1/fireRate)
        fireRate = 1.3f;
    }

    protected override void ShootBullet(Subject shooter)
    {
        int powerUPMultiplier;
        // Check if power shooter is player or enemy
        if (shooter.transform.tag == "Player")
        {
            powerUPMultiplier = PlayerManager.instance.player.GetComponent<Player>().GunPowerUPMultiplier;

            // Substract each shot for player
            CurrentAmmo--;
        }
        else
        {
            powerUPMultiplier = 1;
        }

        for (int bulletCount = 0; bulletCount < bulletShootCount; bulletCount++)
        {
            // Create new bullet with passing Gun there
            Projectile newShoot = Instantiate(bullet, attackPoint.position, attackPoint.rotation);
            newShoot.shooter = shooter.transform;
            newShoot.CurrentGun = this;
            newShoot.PowerUPMultiplier = powerUPMultiplier;
            newShoot.Speed = BulletSpeed;

            // Dispersion of bullets
            newShoot.transform.rotation = Quaternion.RotateTowards(transform.rotation, Random.rotation, angle);
        }
    }
}

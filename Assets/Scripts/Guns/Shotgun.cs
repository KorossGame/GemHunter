using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    private int bulletShootCount = 5;
    private float angle = 15f;

    void Start()
    {
        // Damage
        DamagePerBullet = 24;

        // Bullet speed
        BulletSpeed = 35;

        // Ammo
        ammoInClip = 5;
        maxAmmo = 25;
        ammoLeft = maxAmmo;
        CurrentAmmo = ammoInClip;

        // Range
        EffectiveRange = 5f;
        maxRange = 25f;

        // Reload
        reloadTime = 2.5f;

        // Fire Rate (1/fireRate)
        fireRate = 1.3f;
    }

    protected override void ShootBullet()
    {
        // Check if power up is activated
        int powerUPMultiplier = PlayerManager.instance.player.GetComponent<Player>().GunPowerUPMultiplier;

        // Substract each shot
        CurrentAmmo--;

        for (int bulletCount = 0; bulletCount < bulletShootCount; bulletCount++)
        {
            // Create new bullet with passing Gun there
            Projectile newShoot = Instantiate(bullet, attackPoint.position, attackPoint.rotation);
            newShoot.CurrentGun = this;
            newShoot.PowerUPMultiplier = powerUPMultiplier;
            newShoot.Speed = BulletSpeed;

            // Dispersion of bullets
            newShoot.transform.rotation = Quaternion.RotateTowards(transform.rotation, Random.rotation, angle);
        }
    }
}

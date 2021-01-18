using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    private int bulletShootCount = 5;
    private float angle = 15f;

    void Awake()
    {
        // ID of gun
        ID = 2;

        // Damage
        DamagePerBullet = 24;

        // Bullet speed
        BulletSpeed = 30;

        // Ammo
        ammoInClip = 5;
        MaxAmmo = 25;
        ammoLeft = MaxAmmo;
        CurrentAmmo = ammoInClip;

        // Range
        EffectiveRange = 5f;
        maxRange = 25f;

        // Reload
        reloadTime = 1.25f;

        // Fire Rate (1/fireRate)
        fireRate = 1.4f;

        // Sounds
        shootSound = "ShotgunShootingSound";
        reloadSound = "ShotgunReloadSound";
    }

    protected override void ShootBullet(Subject shooter)
    {
        // Play Sound
        AudioManager.instance.PlaySound(shootSound);

        int powerUPMultiplier;
        // Check if power shooter is player or enemy
        if (shooter.transform.tag == "Player")
        {
            powerUPMultiplier = PlayerManager.instance.player.GetComponent<Player>().GunPowerUPMultiplier;
        }
        else
        {
            powerUPMultiplier = 1;
        }

        // Substract each shot
        CurrentAmmo--;

        // Spawn count of bullets defined by variable
        for (int bulletCount = 0; bulletCount < bulletShootCount; bulletCount++)
        {
            // Create new bullet
            Projectile newShoot = Instantiate(bullet, attackPoint.position, attackPoint.rotation);

            // Set Up Physics for bullet
            newShoot.shooter = shooter.transform;
            newShoot.CurrentGun = this;
            newShoot.PowerUPMultiplier = powerUPMultiplier;
            newShoot.Speed = BulletSpeed;

            // Dispersion of bullets
            newShoot.transform.rotation = Quaternion.RotateTowards(newShoot.transform.rotation, Random.rotation, angle);
        }
    }
}

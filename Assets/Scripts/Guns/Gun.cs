using System;
using System.Collections;
using UnityEngine;

abstract public class Gun : MonoBehaviour
{
    public byte ID { get; protected set; }
    public int DamagePerBullet { get; protected set; }
    protected int CurrentAmmo { get; set; }

    // Ammo block
    protected int ammoInClip;
    public int MaxAmmo { protected get; set; }
    protected int ammoLeft;

    // Range
    public float EffectiveRange { get; protected set; }
    protected float maxRange;

    // Reload
    protected float reloadTime;
    protected float fireRate;
    private float nextShootTime = 1f;
    private bool reloadProcess = false;

    // Switch weapon trigger
    private bool switchProcess = false;
    private float switchTime = 0.5f;

    // Attack point/Bullet point reference
    public Transform attackPoint;

    // Projectile
    public Projectile bullet;
    protected int BulletSpeed { get; set; }

    private void OnEnable()
    {
        // If player switches the weapon while reloading, we need to reset reload process
        reloadProcess = false;
    }

    public void Shoot(Subject shooter)
    {
        // Check if gun have ammo
        if (CurrentAmmo == 0)
        {
            ForceReload();
            return;
        }

        // Check if we can shoot
        if (Time.time >= nextShootTime && !reloadProcess && !switchProcess)
        {
            nextShootTime = Time.time + 1f / fireRate;
            ShootBullet(shooter);
        }
        else return;
    }

    protected virtual void ShootBullet(Subject shooter)
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

        // Create new bullet with passing Gun and Shooter objects there
        Projectile newShoot = Instantiate(bullet, attackPoint.position, attackPoint.rotation);
        newShoot.shooter = shooter.transform;
        newShoot.CurrentGun = this;
        newShoot.PowerUPMultiplier = powerUPMultiplier;
        newShoot.Speed = BulletSpeed;
    }

    public void ForceReload()
    {
        StartCoroutine(Reload());
    }

    public void ForceDelayShot()
    {
        switchProcess = true;
        StartCoroutine(DelayShot());
    }

    protected IEnumerator DelayShot()
    {
        if (switchProcess)
        {
            yield return new WaitForSeconds(switchTime);
            switchProcess = false;
        }
    }

    protected IEnumerator Reload()
    {
        if (!reloadProcess)
        {
            if (ammoLeft != 0 && ammoInClip != CurrentAmmo)
            {

                reloadProcess = true;

                // Play animation + sound

                // Wait for time
                yield return new WaitForSeconds(reloadTime);

                // Check if we have enought ammo for whole clip
                if (ammoInClip > ammoLeft)
                {
                    CurrentAmmo = ammoLeft;
                    ammoLeft = 0;
                }
                else
                {
                    CurrentAmmo = ammoInClip;
                    ammoLeft -= ammoInClip;
                }
                reloadProcess = false;
            }
        }
    }

    public void RefillAmmo()
    {
        ammoLeft = MaxAmmo;
    }

    protected void OnTriggerEnter(Collider col)
    {
        // Pickup the new weapon
        if (col.transform.CompareTag("Player") && gameObject.CompareTag("NewWeapon"))
        {
            WeaponSwitcher playerInventory = PlayerManager.instance.player.GetComponent<Player>().inventory;
            playerInventory.UnlockWeapon(this);

            // Switch weapon tag
            gameObject.tag = "Untagged";
        }
    }
}

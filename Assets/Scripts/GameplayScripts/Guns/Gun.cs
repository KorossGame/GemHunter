using System;
using System.Collections;
using UnityEngine;

abstract public class Gun : MonoBehaviour
{
    public byte ID { get; protected set; }
    public int DamagePerBullet { get; protected set; }
    public int CurrentAmmo { get; protected set; }

    // Ammo block
    protected int ammoInClip;
    public int MaxAmmo { protected get; set; }
    public int ammoLeft;

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

    // Sounds name
    protected string shootSound;
    protected string reloadSound;

    // Muzzle flash
    [SerializeField] protected ParticleSystem muzzleFlash;

    private void OnEnable()
    {
        // If player switches the weapon while reloading, we need to reset reload process
        reloadProcess = false;
    }

    private void OnDisable()
    {
        if (reloadProcess) {
            // We need to stop reload sound till switch the weapon
            AudioManager.instance.StopSound(reloadSound);
        }
    }

    public void Shoot(Subject shooter)
    {
        // Check if gun have ammo
        if (CurrentAmmo == 0)
        {
            ForceReload(shooter);
            return;
        }

        // Check if we can shoot
        if (Time.time >= nextShootTime && !reloadProcess && !switchProcess)
        {
            nextShootTime = Time.time + 1f / fireRate;
            ShootBullet(shooter);
        }
    }

    protected virtual void ShootBullet(Subject shooter)
    {
        // Play shoot sound
        AudioManager.instance.PlaySound(shootSound);

        int powerUPMultiplier;
        // Check if power shooter is player or enemy
        if (shooter.CompareTag("Player"))
        {
            powerUPMultiplier = PlayerManager.instance.player.GetComponent<Player>().GunPowerUPMultiplier;
        }
        else
        {
            powerUPMultiplier = 1;
        }

        // Player and enemies both should have reload process for game balance
        CurrentAmmo--;

        // Create new bullet with passing Gun and Shooter objects there
        Projectile newShoot = Instantiate(bullet, attackPoint.position, attackPoint.rotation);
        newShoot.tag = "Bullet";
        newShoot.shooter = shooter.transform;
        newShoot.CurrentGun = this;
        newShoot.PowerUPMultiplier = powerUPMultiplier;
        newShoot.Speed = BulletSpeed;
    }

    public void ForceReload(Subject shooter)
    {
        StartCoroutine(Reload(shooter));
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

    protected IEnumerator Reload(Subject shooter)
    {
        if (!reloadProcess)
        {
            if (ammoLeft != 0 && ammoInClip != CurrentAmmo)
            {

                reloadProcess = true;

                // Play animation + sound
                AudioManager.instance.PlaySound(reloadSound);

                // Wait for time
                yield return new WaitForSeconds(reloadTime);

                if ((Game.instance.InfiniteAmmoActivated && shooter.CompareTag("Player")) || shooter.CompareTag("Enemy"))
                {
                    CurrentAmmo = ammoInClip;
                }
                else
                {
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

using System;
using System.Collections;
using UnityEngine;

abstract public class Gun : MonoBehaviour
{
    public int DamagePerShot { get; set; }
    public int CurrentAmmo { get; protected set;}

    // Ammo block
    protected int ammoInClip;
    protected int maxAmmo;
    protected int ammoLeft;

    // Range
    public float EffectiveRange { get; protected set; }
    protected float maxRange;

    // Reload
    protected float reloadTime;
    protected float fireRate;
    private float nextShootTime = 0f;
    private bool reloadProcess = false;

    // Attack point/Bullet point reference
    public Transform attackPoint;

    // LayerMask for specific layers (10 stands for enemy)
    private LayerMask enemyLayers = 1 << 10;

    // Projectile
    public Projectile bullet;
    public int BulletSpeed { get; protected set; }

    private void OnEnable()
    {
        // If player switches the weapon while reloading, we need to reset reload process
        reloadProcess = false;
    }

    public void Shoot()
    {
        // Check if power up is activated
        int powerUPMultiplier = PlayerManager.instance.player.GetComponent<Player>().GunPowerUPMultiplier;

        // If weapon is not melee
        if (CurrentAmmo != -1)
        {
            // Check if gun have ammo
            if (CurrentAmmo == 0)
            {
                ForceReload();
                return;
            }

            // Check if we can shoot
            if (Time.time >= nextShootTime)
            {
                nextShootTime = Time.time + 1f / fireRate;

                // Substract each shot 
                CurrentAmmo--;

                Projectile newShoot = Instantiate(bullet, attackPoint.position, attackPoint.rotation);
                newShoot.transform.parent = gameObject.transform;
                newShoot.PowerUPMultiplier = powerUPMultiplier;
                newShoot.Speed = BulletSpeed;
            }
            else return;
        }

        // If weapon is melee
        else
        {
            // Check if we can attack
            if (Time.time >= nextShootTime)
            {
                // Calc next attack
                nextShootTime = Time.time + 1f / fireRate;

                // Detect all objects in sphere for specific layers
                Collider[] hit = Physics.OverlapSphere(attackPoint.position, EffectiveRange, enemyLayers);

                // Apply damage to all objects
                foreach (Collider enemy in hit)
                {
                    Subject target = enemy.transform.GetComponent<Subject>();

                    // Apply damage
                    if (target)
                    {
                        target.applyDamage(DamagePerShot * powerUPMultiplier);
                    }
                }
            }
        }
    }

    public void ForceReload()
    {
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        if (!reloadProcess)
        {
            print(CurrentAmmo);
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
                print("Finished reload");
            }
        }
    }
}

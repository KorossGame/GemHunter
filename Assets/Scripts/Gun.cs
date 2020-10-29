using System;
using UnityEngine;

abstract public class Gun : MonoBehaviour
{
    public int DamagePerShot { get; set; }
    public int CurrentAmmo { get; set; }

    // Ammo block
    protected int ammoInClip;
    protected int maxAmmo;
    protected int ammoLeft;

    // Range
    protected float effectiveRange;
    protected float maxRange;

    // Reload
    protected float reloadTime;
    protected float fireRate;
    private float nextShootTime = 0f;

    // Layer Mask
    LayerMask ignoreObjects;

    public void Shoot(GameObject player)
    {
        // If gun is not melee
        if (CurrentAmmo != -1)
        {
            // Check if gun have ammo
            if (CurrentAmmo == 0)
            {
                Reload();
                return;
            }

            // Check if we can shoot
            if (Time.time >= nextShootTime)
            {
                nextShootTime = Time.time + 1f / fireRate;

                // Substract each shot 
                CurrentAmmo--;

                // Calculate RayCast
                RaycastHit hit;

                //Creating layer mask
                if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, maxRange))
                {
                    Debug.DrawRay(player.transform.position, player.transform.forward * maxRange, Color.red,10);
                    Debug.Log(hit.distance);
                    Subject target = hit.transform.GetComponent<Subject>();

                    // Apply damage
                    if (target)
                    {
                        // Check if shoot is in effective range
                        if (hit.distance <= effectiveRange)
                        {
                            target.applyDamage(DamagePerShot);
                        }
                        else
                        {
                            // newDamage = Damage * 0.85**(Range)
                            int newDamage = Mathf.RoundToInt((float)(DamagePerShot * Math.Pow(0.85f, hit.distance)));
                            target.applyDamage(newDamage);
                        }
                    }
                }
            }
            else return;
        }
        else
        {
            // Melee weapon logic
        }
        
    }

    public void Reload()
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
}

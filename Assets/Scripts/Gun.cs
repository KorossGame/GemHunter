using System;
using UnityEngine;

abstract public class Gun : MonoBehaviour
{
    public int DamagePerShot { get; set; }
    public int CurrentAmmo { get; set; }

    // Is weapon unlocked or not
    public bool Unlocked { get; protected set; }

    // Ammo block
    protected int ammoInClip;
    protected int maxAmmo;
    protected int ammoLeft;

    // Range
    protected float effectiveRange;
    protected float maxRange;

    // Reload
    protected float reloadTime;
    protected float cooldownTime;

    public void Shoot(GameObject player)
    {
        // Check if gun have ammo
        if (CurrentAmmo == 0)
        {
            Reload();
            return;
        }

        // Substract each shot 
        CurrentAmmo--;

        // Calculate RayCast
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, maxRange))
        {
            Subject target = hit.transform.GetComponent<Subject>();

            // Apply damage
            if (target)
                target.applyDamage(DamagePerShot);
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

    public void SwitchWeapon(int currentWeapon, int newCurrentWeapon)
    {
         /*
          * if player picked new weapon, it becomes enabled
          * if player wants to change weapons by mouse wheel - switch to previous/next enabled weapon
          * if player wants to change weapon by numbers - change to specific weapon if enabled
          */
    }

    public void UnlockWeapon(int weapon)
    {
        Unlocked = true;
    }
}

using UnityEngine;

public class Pistol : Gun
{
    void Start()
    {
        // Damage
        DamagePerShot = 15;

        // Ammo
        ammoInClip = 999999;
        maxAmmo = 999999;
        ammoLeft = ammoInClip;
        CurrentAmmo = ammoInClip;

        // Range
        effectiveRange = 5f;
        maxRange = 25f;

        // Reload
        reloadTime = 2f;

        // Fire Rate (1/fireRate)
        fireRate = 4f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Melee : Gun
{
    void Start()
    {
        // Damage
        DamagePerShot = 5;

        // Ammo
        CurrentAmmo = -1;

        // Range
        effectiveRange = 1f;
        maxRange = 1f;

        // Reload
        fireRate = 2f;
    }
}

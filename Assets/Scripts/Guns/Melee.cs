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
        effectiveRange = 10f;
        maxRange = 200f;

        // Reload
        reloadTime = 2f;
        fireRate = 0.15f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fast : Enemy
{
    void Awake()
    {
        // HP and Speed of movement
        HP = 10;
        Speed = 15f;

        // Weapon chances
        weaponChance = new double[] { 100, 0, 0, 0, 0 };
    }
}

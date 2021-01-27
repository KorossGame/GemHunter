using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : Enemy
{
    void Awake()
    {
        // HP and Speed of movement
        HP = 50;
        Speed = 4f;

        // Weapon chances
        weaponChance = new double[] { 70, 15, 9, 4.9, 1 };
    }
}

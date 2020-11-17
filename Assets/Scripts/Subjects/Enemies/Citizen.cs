using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : Enemy
{
    void Awake()
    {
        // HP and Speed of movement
        HP = 50;
        Speed = 5f;

        // Weapon chances
        weaponChance = new double[] { 70, 15, 10, 4.9, 0.1 };
    }
}

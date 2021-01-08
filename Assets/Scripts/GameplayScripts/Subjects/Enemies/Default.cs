using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Default : Enemy
{
    void Awake()
    {
        // HP and Speed of movement
        HP = 45;
        Speed = 5f;

        // Weapon chances
        weaponChance = new double[] { 100, 0, 0, 0, 0 };
    }
}

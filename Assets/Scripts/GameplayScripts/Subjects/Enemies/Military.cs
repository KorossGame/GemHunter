using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Military : Enemy
{

    void Awake()
    {
        // HP and Speed of movement
        HP = 100;
        Speed = 6.4f;

        // Weapon chances
        weaponChance = new double[] { 0, 20, 30, 40, 10 };
    }
}

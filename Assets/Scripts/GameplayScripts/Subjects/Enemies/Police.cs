using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : Enemy
{

    void Awake()
    {
        // HP and Speed of movement
        HP = 70;
        Speed = 5.6f;

        // Weapon chances
        weaponChance = new double[] { 30, 40, 9, 15, 5 };
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    private byte phase;

    void Awake()
    {
        // HP and Speed of movement
        HP = 500;
        Speed = 5f;

        // Weapon chances
        weaponChance = new double[] { 0, 0, 0, 0, 0 };
    }
}

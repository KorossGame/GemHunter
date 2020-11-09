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

        // Weapon
        meleeChance = 0;
        pistolChance = 0;
        shotgunChance = 0;
        assaultChance = 0;
        RPGChance = 0;
    }
}

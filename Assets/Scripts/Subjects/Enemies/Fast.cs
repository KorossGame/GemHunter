using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fast : Enemy
{
    void Start()
    {
        // HP and Speed of movement
        HP = 10;
        Speed = 15f;

        // Weapon
        meleeChance = 100;
        pistolChance = 0;
        shotgunChance = 0;
        assaultChance = 0;
        RPGChance = 0;
    }
}

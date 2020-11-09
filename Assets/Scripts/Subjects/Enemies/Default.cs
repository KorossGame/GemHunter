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

        // Weapon
        meleeChance = 100;
        pistolChance = 0;
        shotgunChance = 0;
        assaultChance = 0;
        RPGChance = 0;
    }
}

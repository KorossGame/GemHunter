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

        // Weapon
        meleeChance = 70;
        pistolChance = 85;
        shotgunChance = 95;
        assaultChance = 99.9;
        RPGChance = 100;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : Enemy
{
    void Start()
    {
        // HP and Speed of movement
        HP = 50;
        Speed = 5f;

        // Weapon
        meleeChance = 70;
        pistolChance = 15;
        ShotgunChance = 10;
        AssaultChance = 4.9;
        RPGChance = 0.1;
    }
}

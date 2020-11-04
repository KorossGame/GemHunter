using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Military : Enemy
{

    void Start()
    {
        // HP and Speed of movement
        HP = 100;
        Speed = 8f;

        // Weapon
        meleeChance = 0;
        pistolChance = 20;
        ShotgunChance = 30;
        AssaultChance = 45;
        RPGChance = 5;
    }
}

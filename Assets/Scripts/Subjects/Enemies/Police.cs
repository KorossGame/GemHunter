using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : Enemy
{

    void Start()
    {
        // HP and Speed of movement
        HP = 70;
        Speed = 7f;

        // Weapon
        meleeChance = 30;
        pistolChance = 40;
        ShotgunChance = 14;
        AssaultChance = 15;
        RPGChance = 1;
    }
}

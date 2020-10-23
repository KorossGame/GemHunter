using System;
using UnityEngine;

public class Player : Subject
{

    void Start()
    {
        WeaponEquiped = new Melee();
        Speed = 10f;
    }
}

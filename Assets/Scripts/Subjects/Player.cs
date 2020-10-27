using System;
using UnityEngine;

public class Player : Subject
{
    void Start()
    {
        WeaponEquiped = new Pistol();
        Speed = 10f;
    }
}

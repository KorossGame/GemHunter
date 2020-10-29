using System;
using UnityEngine;

public class Player : Subject
{
    public WeaponSwitcher inventory;

    void Start()
    {
        Speed = 10f;
        HP = 100;
    }

    protected override void Die()
    {
        print("wadawd");
    }
}
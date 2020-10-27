using System;
using UnityEngine;

public class Player : Subject
{
    public WeaponSwitcher inventory;

    void Start()
    {
        Speed = 10f;
    }

    protected override void Die()
    {
        print("wadawd");
    }
}
using System;
using UnityEngine;

public class Player : Subject
{
    public WeaponSwitcher inventory;
    public int GunPowerUPMultiplier { get; set; } = 1;

    void Start()
    {
        Speed = 10f;
        HP = 100;
    }

    protected override void Die()
    {
        print("Player is dead");
    }
    


}
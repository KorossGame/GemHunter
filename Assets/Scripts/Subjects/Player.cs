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

    public override void applyDamage(int damage)
    {
        // Play custom animation and sound

        // Calc damage
        base.applyDamage(damage);
    }

    protected override void Die()
    {
        // Play custom animation and sound

        // Die
        base.Die();
    }
}
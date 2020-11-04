using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public Gun defaultWeapon;
    public Gun WeaponEquiped { get; set; }
    private Transform holderPoint;

    void Start()
    {
        holderPoint = gameObject.transform;
        if (!WeaponEquiped)
        {
            EquipWeapon(defaultWeapon);
        }
    }

    public void EquipWeapon(Gun toEquip)
    {
        if (WeaponEquiped)
        {
            Destroy(WeaponEquiped.gameObject);
        }
        WeaponEquiped = Instantiate(toEquip, holderPoint.position, holderPoint.rotation, holderPoint);
    }

    
}

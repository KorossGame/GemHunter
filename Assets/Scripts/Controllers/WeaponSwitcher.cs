using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public Gun defaultWeapon;
    public Gun weaponEquiped;
    private Transform holderPoint;

    void Start()
    {
        holderPoint = gameObject.transform;
        if (!weaponEquiped)
        {
            EquipWeapon(defaultWeapon);
        }
    }

    public void EquipWeapon(Gun toEquip)
    {
        if (weaponEquiped)
        {
            Destroy(weaponEquiped.gameObject);
        }
        weaponEquiped = Instantiate(toEquip, holderPoint.position, holderPoint.rotation);
        weaponEquiped.transform.parent = holderPoint;
    }

    
}

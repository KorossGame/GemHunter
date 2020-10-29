using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public int selectedWeapon = 0;
    public int previousWeapon;
    public Gun weaponEquiped;

    void Start()
    {
        SelectWeapon();
    }

    public void SelectWeapon()
    {
        previousWeapon = selectedWeapon;

        int i = 0;
        foreach (Transform weapon in transform)
        {
            // Only active elements might be changed
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                weaponEquiped = weapon.gameObject.GetComponent<Gun>();
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    
}

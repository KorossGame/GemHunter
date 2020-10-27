using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public byte selectedWeapon = 0;
    private bool initialized = false;

    void Start()
    {
        SelectWeapon();
    }

    public void SelectWeapon()
    {
        checkSelectedWeapon();
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (!initialized)
            {
                if (i == selectedWeapon)
                    weapon.gameObject.SetActive(true);
                else
                    weapon.gameObject.SetActive(false);
            }
            else 
            {
                // Only active elements might be changed
                if (i == selectedWeapon && weapon.gameObject.GetComponent<Gun>().Unlocked)
                {
                    weapon.gameObject.SetActive(true);
                }
                else
                {
                    weapon.gameObject.SetActive(false);
                }
            }
            i++;
        }
        initialized = true;
    }

    private void checkSelectedWeapon()
    {
        if (selectedWeapon > 5)
            selectedWeapon = 5;
    }
}

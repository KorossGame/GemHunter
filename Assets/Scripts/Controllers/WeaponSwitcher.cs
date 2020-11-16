using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public Gun WeaponEquiped { get; private set; }
    public Gun[] availableGuns;

    void Start()
    {
        availableGuns = new Gun[GunManager.instance.guns.Length];
    }

    public void EquipWeapon(int gunIndex)
    {
        // Check if Gun present
        if (availableGuns[gunIndex])
        {
            // Disable current weapon
            if (WeaponEquiped)
            {
                WeaponEquiped.gameObject.SetActive(false);
            }

            // Activate new weapon
            WeaponEquiped = availableGuns[gunIndex];

            // Reset position and rotation of gun
            WeaponEquiped.transform.localPosition = Vector3.zero;
            WeaponEquiped.transform.localRotation = new Quaternion(0, 0, 0, 0);

            // Set weapon active
            WeaponEquiped.gameObject.SetActive(true);
        }
    }

    public void UnlockWeapon(Gun toUnlock)
    {
        foreach (Gun gun in availableGuns)
        {
            if (gun == toUnlock) return;
        }

        // Place gun in array of guns by guns ID
        availableGuns[toUnlock.ID] = toUnlock;

        // Move gun to inventory of player
        toUnlock.transform.parent = gameObject.transform;
        toUnlock.gameObject.SetActive(false);
    }

    public int GetCurrentWeaponID()
    {
        for (int index=0; index < availableGuns.Length; index++)
        {
            if (availableGuns[index] == WeaponEquiped)
            {
                return index;
            }
        }
        return -1;
    }

    
}

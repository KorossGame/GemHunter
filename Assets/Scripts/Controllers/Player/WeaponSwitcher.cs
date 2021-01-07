﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public Gun WeaponEquiped { get; private set; }
    public Gun[] availableGuns;

    private bool assignedEvent = false;

    private void OnDisable()
    {
        EventManager.instance.AllWeaponsUnlocked -= TutorialGameManager.instance.EnemyPhase;
    }

    private void Start()
    {
        availableGuns = new Gun[GunManager.instance.guns.Length];
    }

    private void Update()
    {
        if (!assignedEvent && EventManager.instance && TutorialGameManager.instance)
        {
            EventManager.instance.AllWeaponsUnlocked += TutorialGameManager.instance.EnemyPhase;
            assignedEvent = true;
        }
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

            // Activate switch process
            WeaponEquiped.ForceDelayShot();
        }
    }

    public void UnlockWeapon(Gun toUnlock)
    {
        // If player has weapon don't need to unlock twice
        foreach (Gun gun in availableGuns)
        {
            if (gun == toUnlock) return;
        }

        // Place gun in array of guns by guns ID
        availableGuns[toUnlock.ID] = toUnlock;

        // Move gun to inventory of player
        toUnlock.transform.parent = gameObject.transform;
        
        // If nothing is equiped - equip new weapon
        if (!WeaponEquiped)
        {
            EquipWeapon(toUnlock.ID);
        }
        else
        {
            toUnlock.gameObject.SetActive(false);
        }

        // Check if player unlocked all the weapons
        checkUnlockedWeapons();
    }

    private void checkUnlockedWeapons()
    {
        // Check if any gun is not unlocked yet
        foreach (Gun gun in availableGuns)
        {
            if (gun == null) return;
        }

        // If all guns were unlocked - trigger event
        EventManager.instance.WeaponsUnlockedEvent();
    }

    public int GetCurrentWeaponID()
    {
        for (int index = 0; index < availableGuns.Length; index++)
        {
            if (availableGuns[index] == WeaponEquiped)
            {
                return index;
            }
        }
        return -1;
    }

    public int getNextWeapon()
    {
        int index = GetCurrentWeaponID();

        for (int i = index + 1; i < availableGuns.Length; i++)
        {
            if (availableGuns[i])
            {
                return i;
            }
        }

        for (int i = 0; i < index; i++)
        {
            if (availableGuns[i])
            {
                return i;
            }
        }

        return index;
    }

    public int getPreviousWeapon()
    {
        int index = GetCurrentWeaponID();

        for (int i = index - 1; i >= 0; i--)
        {
            if (availableGuns[i])
            {
                return i;
            }
        }

        for (int i = availableGuns.Length - 1; i > index; i--)
        {
            if (availableGuns[i])
            {
                return i;
            }
        }

        return index;
    }
}

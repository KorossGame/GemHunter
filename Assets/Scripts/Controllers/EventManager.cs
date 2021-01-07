using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    private void Awake()
    {
        instance = this;
    }


    /* Different Delegates and Events */

    // Event when players picks up all the weapons
    public delegate void WeaponsUnlocked();
    public event WeaponsUnlocked AllWeaponsUnlocked;

    public void WeaponsUnlockedEvent() {
        AllWeaponsUnlocked?.Invoke();
    }
}

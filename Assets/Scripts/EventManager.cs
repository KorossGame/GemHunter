using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    /* Different Delegates and Events */

    // Event when players picks up all the weapons
    public delegate void WeaponsUnlocked();
    public static WeaponsUnlocked AllWeaponsUnlocked;

    public static void WeaponsUnlockedEvent() {
        AllWeaponsUnlocked?.Invoke();
    }
}

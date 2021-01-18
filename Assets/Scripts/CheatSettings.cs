using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatSettings : MonoBehaviour
{
    [Header("Cheats")]
    [SerializeField] private Toggle godMode;
    [SerializeField] private Toggle infiniteAmmo;

    private void OnEnable()
    {
        if (Game.instance != null)
        {
            SetGodMode(Game.instance.GodModeActivated);
            SetInfiniteAmmo(Game.instance.InfiniteAmmoActivated);
        }
    }

    public void SetGodMode(bool godModeValue)
    {
        // Change UI if players exits from game, but cheats were activated
        godMode.isOn = godModeValue;

        Game.instance.GodModeActivated = godModeValue;
    }

    public void SetInfiniteAmmo(bool infiniteAmmoValue)
    {
        // Change UI if players exits from game, but cheats were activated
        infiniteAmmo.isOn = infiniteAmmoValue;

        Game.instance.InfiniteAmmoActivated = infiniteAmmoValue;
    }
}

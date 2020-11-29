using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAmmoBox : AmmoBox
{
    protected override void PickUP()
    {
        if (playerInventory.availableGuns[0])
        {
            playerInventory.availableGuns[0].RefillAmmo();
        }
        base.PickUP();
    }
}

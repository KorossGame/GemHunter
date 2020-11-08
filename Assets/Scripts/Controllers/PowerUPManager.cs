using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUPManager : MonoBehaviour
{
    public static PowerUPManager instance;

    public PowerUP damageBonus;
    public PowerUP godBonus;
    public PowerUP speedBonus;

    void Awake()
    {
        instance = this;
    }
}

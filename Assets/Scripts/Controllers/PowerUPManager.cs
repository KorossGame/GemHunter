using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUPManager : MonoBehaviour
{
    public static PowerUPManager instance;

    public PowerUP[] powerUPs;

    void Awake()
    {
        instance = this;
    }
}

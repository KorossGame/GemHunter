using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public static GunManager instance;

    public Gun[] guns;

    void Awake()
    {
        instance = this;
    }
}

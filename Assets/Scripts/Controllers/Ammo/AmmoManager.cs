using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    public static AmmoManager instance;
    public AmmoBox[] ammoBoxes;

    void Awake()
    {
        instance = this;
    }
}

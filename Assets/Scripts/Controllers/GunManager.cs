using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public static GunManager instance;

    public Gun meleeWeapon;
    public Gun pistolWeapon;
    public Gun shotgunWeapon;
    public Gun assaultWeapon;
    public Gun RPGWeapon;

    void Awake()
    {
        instance = this;
    }
}

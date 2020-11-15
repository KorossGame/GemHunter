using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AmmoBox : MonoBehaviour
{
    protected virtual void PickUP()
    {
        // Destory an object of ammo box
        Destroy(gameObject);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUP();
        }
    }
}

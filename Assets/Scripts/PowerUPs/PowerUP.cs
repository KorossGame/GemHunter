using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class PowerUP : MonoBehaviour
{
    protected float activeTime = 15f;
    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    protected virtual IEnumerator Pickup(Collider player)
    {
        yield return new WaitForSeconds(activeTime);
    }
}

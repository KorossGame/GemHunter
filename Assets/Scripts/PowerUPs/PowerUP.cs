using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
abstract public class PowerUP : MonoBehaviour
{
    protected float activeTime = 15f;
    public event Action OnPickUP;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPickUP?.Invoke();
            StartCoroutine(Pickup(other));
        }
    }

    protected virtual IEnumerator Pickup(Collider player)
    {
        yield return new WaitForSeconds(activeTime);
    }
}

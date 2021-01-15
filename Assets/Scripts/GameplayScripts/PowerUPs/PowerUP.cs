using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
abstract public class PowerUP : MonoBehaviour
{
    protected float activeTime = 15f;

    // Events
    public event Action OnPickUP;
    public event Action OnDestroy;

    // Auto-destroy in case no picking up in time
    private Coroutine destroy;
    private float destroyTime = 10f;

    private void OnEnable()
    {
        destroy = StartCoroutine(DestroyMe());
    }
    
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // If player enters the level - power up should stay
            DontDestroyOnLoad(gameObject);
            StopCoroutine(destroy);
            OnPickUP?.Invoke();
            AudioManager.instance.PlaySound("PowerUPPickUP");
            StartCoroutine(Pickup(other));
        }
    }

    protected virtual IEnumerator Pickup(Collider player)
    {
        yield break;
    }

    private IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(destroyTime);
        OnDestroy?.Invoke();
        Destroy(gameObject);
    }
}

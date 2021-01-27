using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AmmoBox : MonoBehaviour
{
    protected WeaponSwitcher playerInventory;
    protected int weaponID;

    // Events
    public event Action OnPickUP;
    public event Action OnDestroy;

    // Auto-destroy in case no picking up in time
    private Coroutine destroy;
    private float destroyTime = 10f;

    protected void Start()
    {
        findPlayerInventory();
        destroy = StartCoroutine(DestroyMe());
    }

    protected void Update()
    {
        if (!playerInventory)
        {
            findPlayerInventory();
        }
    }

    private void findPlayerInventory()
    {
        try
        {
            playerInventory = PlayerManager.instance.player.GetComponent<Player>().inventory;
        }
        catch (NullReferenceException)
        {
            playerInventory = null;
        }
    }

    protected virtual void PickUP()
    {
        StopCoroutine(destroy);

        AudioManager.instance.PlaySound("AmmoPickUP");
        OnPickUP?.Invoke();

        // If player has this type of weapon - add ammo
        if (playerInventory.availableGuns[weaponID])
        {
            playerInventory.availableGuns[weaponID].RefillAmmo();
        }

        // Destroy a ammo box object
        Destroy(gameObject);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUP();
        }
    }

    private IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(destroyTime);
        OnDestroy?.Invoke();
        Destroy(gameObject);
    }
}

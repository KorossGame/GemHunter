using UnityEngine;
using System.Collections;

public class DamageBonus : PowerUP
{
    public GameObject pickupEffect;
    private int multiplier = 2;

    /*protected override IEnumerator Pickup(Collider player)
    {

        // Create particles
        //Instantiate(pickupEffect, transform.position, transform.rotation);

        // Apply powerup
        Player playerGameObject = player.GetComponent<Player>();
        playerGameObject.WeaponEquiped.DamagePerShot *= multiplier;
        Debug.Log(playerGameObject.WeaponEquiped.DamagePerShot);

        // Get collider and mesh renderer components and Disable them
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

        // Wait particular amount of time
        yield return new WaitForSeconds(activeTime);

        // Reverse powerup
        playerGameObject.WeaponEquiped.DamagePerShot /= multiplier;
        Debug.Log(playerGameObject.WeaponEquiped.DamagePerShot);

        // Delete powerUP object
        Destroy(gameObject);
    }*/
}

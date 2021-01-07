using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : PowerUP
{
    public GameObject pickupEffect;

    protected override IEnumerator Pickup(Collider player)
    {

        // Create particles
        //Instantiate(pickupEffect, transform.position, transform.rotation);

        // Get Player class from visual object
        Player playerGameObject = player.GetComponent<Player>();

        // Stop previous powerups
        StopAllCoroutines();

        // Apply powerup
        playerGameObject.GodMode = true;

        // Get collider and mesh renderer components and Disable them
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

        // Wait particular amount of time
        yield return new WaitForSeconds(activeTime);

        // Reverse powerup
        playerGameObject.GodMode = false;

        // Delete powerUP object
        Destroy(gameObject);
    }
}

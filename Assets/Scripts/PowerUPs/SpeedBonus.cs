using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBonus : PowerUP
{
    public GameObject pickupEffect;
    private byte multiplier = 2;

    protected override IEnumerator Pickup(Collider player)
    {
        // Create particles
        //Instantiate(pickupEffect, transform.position, transform.rotation);

        // Get Player class from visual object
        Player playerGameObject = player.GetComponent<Player>();

        // Get collider and mesh renderer components for this class object and disable them
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

        // Activate powerUP
        playerGameObject.Speed *= 2;

        // Wait particular amount of time
        yield return new WaitForSeconds(activeTime);

        // Reverse PowerUP
        playerGameObject.Speed /= 2;

        // Delete PowerUP Object
        Destroy(gameObject);
    }
}

using UnityEngine;
using System.Collections;

public class DamageBonus : PowerUP
{
    public GameObject pickupEffect;
    private byte multiplier = 2;

    protected override IEnumerator Pickup(Collider player)
    {

        // Create particles
        //Instantiate(pickupEffect, transform.position, transform.rotation);

        // Get Player class from visual object
        Player playerGameObject = player.GetComponent<Player>();

        // Stop previous powerups
        StopAllCoroutines();
        playerGameObject.GunPowerUPMultiplier *= multiplier;

        // Get collider and mesh renderer components and Disable them
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

        // Wait particular amount of time
        yield return new WaitForSeconds(activeTime);

        // Reverse powerup
        playerGameObject.GunPowerUPMultiplier /= multiplier;

        // Delete powerUP object
        Destroy(gameObject);
    }
}

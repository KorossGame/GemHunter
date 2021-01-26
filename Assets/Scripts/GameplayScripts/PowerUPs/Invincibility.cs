using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : PowerUP
{

    protected override IEnumerator Pickup(Collider player)
    {

        // Get Player class from visual object
        Player playerGameObject = player.GetComponent<Player>();

        // Activate UI
        playerGameObject.godPowerUP.SetActive(true);

        // Apply powerup
        if (playerGameObject.GodMode == false)
        {
            playerGameObject.GodMode = true;
        }

        // Get collider and mesh renderer components and Disable them
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

        // Wait particular amount of time
        yield return new WaitForSeconds(activeTime);

        // Reverse powerup
        if (playerGameObject.GodMode)
        {
            playerGameObject.GodMode = false;
        }

        playerGameObject.godPowerUP.SetActive(false);

        // Delete powerUP object
        Destroy(gameObject);
    }
}

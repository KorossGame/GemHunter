using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBonus : PowerUP
{
    private float multiplier = 1.75f;

    protected override IEnumerator Pickup(Collider player)
    {
        // Get Player class from visual object
        Player playerGameObject = player.GetComponent<Player>();

        // Activate UI
        playerGameObject.speedPowerUP.SetActive(true);

        // Get collider and mesh renderer components for this class object and disable them
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

        // Activate powerUP
        if (playerGameObject.SpeedBonusMultiplier == 1)
        {
            playerGameObject.SpeedBonusMultiplier *= multiplier;
        }

        // Wait particular amount of time
        yield return new WaitForSeconds(activeTime);

        // Reverse PowerUP
        if (playerGameObject.SpeedBonusMultiplier > 1)
        {
            playerGameObject.SpeedBonusMultiplier /= multiplier;
        }

        playerGameObject.speedPowerUP.SetActive(false);

        // Delete PowerUP Object
        Destroy(gameObject);
    }
}

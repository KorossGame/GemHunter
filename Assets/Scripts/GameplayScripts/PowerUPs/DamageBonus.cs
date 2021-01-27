using UnityEngine;
using System.Collections;

public class DamageBonus : PowerUP
{
    private byte multiplier = 2;

    protected override IEnumerator Pickup(Collider player)
    {
        // Get Player class from visual object
        Player playerGameObject = player.GetComponent<Player>();

        // Activate UI
        playerGameObject.damagePowerUP.SetActive(true);

        // Activate new power UP
        if (playerGameObject.GunPowerUPMultiplier == 1)
        {
            playerGameObject.GunPowerUPMultiplier *= multiplier;
        }
        
        // Get collider and mesh renderer components and Disable them
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

        // Wait particular amount of time
        yield return new WaitForSeconds(activeTime);

        // Reverse powerup
        if (playerGameObject.GunPowerUPMultiplier > 1)
        {
            playerGameObject.GunPowerUPMultiplier /= multiplier;
        }

        playerGameObject.damagePowerUP.SetActive(false);

        // Delete powerUP object
        Destroy(gameObject);
    }
}

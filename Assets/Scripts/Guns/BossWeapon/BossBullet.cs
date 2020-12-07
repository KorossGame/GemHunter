using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : Projectile
{
    // Boss does not have any weapon, we need to hardcode the damage
    byte damage = 35;

    private void Start()
    {
        // Get Rigidbody reference
        rb = GetComponent<Rigidbody>();

        // Call self-destruction after 10 seconds
        Despawn();

        // Only boss will shoot such projectiles
        enemyLayers |= 1 << 10;
        enemyLayers = ~enemyLayers;
    }

    protected override void DetectCollisions(float moveDistance)
    {
        /* As boss bullets are huge spheres we can not use raycast */

        // Detect all objects in sphere for specific layers
        Collider[] hit = Physics.OverlapSphere(transform.position, transform.localScale.x, enemyLayers);

        // Use raycast with layer mask (only against colliders in specific layers)
        foreach (Collider enemy in hit)
        {
            // Get a subject we hit
            Subject target = enemy.transform.GetComponent<Subject>();

            if (target)
            {
                // Apply damage depending on lenght of vector
                target.applyDamage(damage);
            }

            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}

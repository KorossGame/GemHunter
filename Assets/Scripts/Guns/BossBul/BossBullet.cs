using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : Projectile
{
    byte damage = 35;

    void Start()
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
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Use raycast with layer mask (only against colliders in specific layers)
        if (Physics.Raycast(ray, out hit, moveDistance, enemyLayers))
        {
            // Get a subject we hit
            Subject target = hit.transform.GetComponent<Subject>();

            if (target)
            {
                // Length of 3D vector
                float hitDistance = Vector3.Distance(target.transform.position, shooter.position);

                // Apply damage depending on lenght of vector
                target.applyDamage(damage);
            }

            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}

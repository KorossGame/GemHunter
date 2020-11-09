using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // LayerMask for specific layers
    private LayerMask enemyLayers;

    // Speed of bullet
    public float Speed { private get; set; } = 10f;

    public Gun CurrentGun { private get; set; }
    public int PowerUPMultiplier { private get; set; }

    private float livingEntityTime = 10f;

    // Reference to player
    public Transform shooter;

    // Reference to rigidbody
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Despawn();
    }

    void FixedUpdate()
    {
        float moveDistance = Speed * Time.fixedDeltaTime;
        DetectCollisions(moveDistance);
        rb.MovePosition(transform.position + (transform.forward * moveDistance));
    }

    void DetectCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Ignore the shooter layer
        if (shooter.transform.tag == "Player")
        {
            enemyLayers = 1 << 9;
        }
        else
        {
            enemyLayers = 1 << 10;
        }

        // Use raycast with layer mask (only against colliders in specific layers)
        if (Physics.Raycast(ray, out hit, moveDistance, ~enemyLayers))
        {
            Subject target = hit.transform.GetComponent<Subject>();

            if (target)
            {
                // Length of 2D vector
                float hitDistance = Vector2.Distance(target.transform.position, shooter.position);

                // Apply damage depending on lenght of vector
                if (hitDistance <= CurrentGun.EffectiveRange)
                {
                    target.applyDamage(CurrentGun.DamagePerBullet * PowerUPMultiplier);
                }
                else
                {
                    // Formula: newDamage = Damage * 0.85**(Range)
                    int newDamage = Mathf.RoundToInt((float)(CurrentGun.DamagePerBullet * Math.Pow(0.85f, hitDistance / 2)));
                    target.applyDamage(newDamage * PowerUPMultiplier);
                }

            }
            
            // Destroy the projectile
            Destroy(gameObject);
        }
    }

    void Despawn()
    {
        Destroy(gameObject, livingEntityTime);
    }
}

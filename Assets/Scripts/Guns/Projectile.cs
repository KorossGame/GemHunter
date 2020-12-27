using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // LayerMask for specific layers
    public LayerMask enemyLayers;

    // Speed of bullet
    public float Speed { private get; set; } = 10f;
    private float moveDistance;

    public Gun CurrentGun { private get; set; }
    public int PowerUPMultiplier { private get; set; }

    protected float livingEntityTime = 10f;

    // Reference to player
    public Transform shooter;

    // Reference to rigidbody
    protected Rigidbody rb;

    private void Start()
    {
        // Get Rigidbody reference
        rb = GetComponent<Rigidbody>();

        // Call self-destruction after 10 seconds
        Despawn();

        // Invert layer mask to ignore layers
        if (shooter.tag == "Player")
        {
            enemyLayers |= 1 << 9;
        }
        else
        {
            enemyLayers |= 1 << 10;
        }
        enemyLayers = ~enemyLayers;
    }

    protected void FixedUpdate()
    {
        if (gameObject)
        {
            moveDistance = Speed * Time.fixedDeltaTime;
            DetectCollisions(moveDistance);
            rb.MovePosition(transform.position + (transform.forward * moveDistance));
        }
    }

    protected virtual void DetectCollisions(float moveDistance)
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

    protected void Despawn()
    {
        Destroy(gameObject, livingEntityTime);
    }
}

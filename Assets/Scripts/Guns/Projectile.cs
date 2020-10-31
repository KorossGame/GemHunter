using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // LayerMask for specific layers (10 stands for enemy)
    private LayerMask enemyLayers = 1 << 10;

    public float Speed { private get; set; } = 10f;

    private Gun currentGun;
    public int PowerUPMultiplier { private get; set; }

    private Transform player;

    void Start()
    {
        player = PlayerManager.instance.player.transform;
        currentGun = gameObject.transform.parent.GetComponent<Gun>();
    }

    void Update()
    {
        float moveDistance = Speed * Time.deltaTime;
        DetectCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }

    void DetectCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Use raycast with layer mask (only against colliders in specific layers)
        if (Physics.Raycast(ray, out hit, moveDistance, enemyLayers))
        {
            Subject target = hit.transform.GetComponent<Subject>();

            // Length of 2D vector
            float hitDistance = Vector2.Distance(target.transform.position, player.position);

            // Apply damage depending on lenght of vector
            if (target)
            {
                // Check if shoot is in effective range
                if (hitDistance <= currentGun.EffectiveRange)
                {
                    target.applyDamage(currentGun.DamagePerShot * PowerUPMultiplier);
                }
                else
                {
                    // Formula: newDamage = Damage * 0.85**(Range)
                    int newDamage = Mathf.RoundToInt((float)(currentGun.DamagePerShot * Math.Pow(0.85f, hitDistance / 2)));
                    target.applyDamage(newDamage * PowerUPMultiplier);
                }
                
            }

            // Destroy the projectile
            GameObject.Destroy(gameObject);
        }
    }
}

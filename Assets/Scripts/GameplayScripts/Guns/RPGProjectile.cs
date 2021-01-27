using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGProjectile : Projectile
{
    public GameObject explosion;
    private float blastRadius = 10f;

    protected override void DetectCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;


        // Use raycast with layer mask (only against colliders in specific layers)
        if (Physics.Raycast(ray, out hit, moveDistance * 5, enemyLayers))
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
                    // Formula: newDamage = Damage * 0.85**(Range/2)
                    int newDamage = Mathf.RoundToInt((float)(CurrentGun.DamagePerBullet * Mathf.Pow(0.85f, hitDistance / 2)));
                    target.applyDamage(newDamage * PowerUPMultiplier);
                }
            }

            AudioManager.instance.PlaySound("RPGExplosion");
            Instantiate(explosion, transform.position, Quaternion.identity);

            Collider[] hitCol = Physics.OverlapSphere(transform.position, blastRadius);
            foreach (Collider col in hitCol)
            {
                target = col.transform.GetComponent<Subject>();

                if (target)
                {
                    float currentBlastRadius = Vector3.SqrMagnitude(col.transform.position - transform.position);
                    target.applyDamage((int)(CurrentGun.DamagePerBullet / currentBlastRadius*2.5f));
                }
            }
            
            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}

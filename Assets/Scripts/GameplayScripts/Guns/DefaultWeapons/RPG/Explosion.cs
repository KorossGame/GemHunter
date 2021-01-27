using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private ParticleSystem explosion;

    private void OnEnable()
    {
        explosion = GetComponent<ParticleSystem>();
        Destroy(gameObject, explosion.main.duration);
        explosion.Play();
    }
}

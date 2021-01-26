using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEventManager : MonoBehaviour
{
    private string[] animations = new string[] { "RandomEnemyFall", "RunForCube" };
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        // Set random seed
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        PlayRandomAnimation();
    }

    public void PlayIdle()
    {
        animator.Play("Idle");
    }

    public void PlayRandomAnimation()
    {
        animator.Play(animations[Random.Range(0, animations.Length)]);
    }
}

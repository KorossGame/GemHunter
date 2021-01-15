﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    private Transform target;
    public float movementSpeed;
    public Vector3 targetOffset;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (target)
        {
            MoveCamera();
            transform.LookAt(target);
        }
        else
        {
            if (PlayerManager.instance.player)
            {
                target = PlayerManager.instance.player.transform;
            }
        }
    }

    private void MoveCamera()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, movementSpeed * Time.deltaTime);
    }
}

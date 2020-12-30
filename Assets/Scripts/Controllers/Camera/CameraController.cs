using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;
    public float movementSpeed;
    public Vector3 targetOffset;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
    }

    void Update()
    {
        if (target)
        {
            MoveCamera();
            transform.LookAt(target);
        }
    }

    void MoveCamera()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, movementSpeed * Time.deltaTime);
    }
}

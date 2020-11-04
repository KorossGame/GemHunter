using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;
    public float movementSpeed;
    public Vector3 targetOffset = new Vector3(0,0,0);

    void Start()
    {
        target = PlayerManager.instance.player.transform;
    }

    void FixedUpdate()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, movementSpeed * Time.deltaTime);
    }
}

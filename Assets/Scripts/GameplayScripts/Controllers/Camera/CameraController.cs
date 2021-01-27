using System.Collections;
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

    public IEnumerator Shake(float duration=0.1f, float magnitude=0.05f)
    {
        float elapsed = 0.0f;
        Vector3 originPos = transform.position;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            
            transform.position = new Vector3(x, y, 0) + originPos;

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = originPos;
    }
}

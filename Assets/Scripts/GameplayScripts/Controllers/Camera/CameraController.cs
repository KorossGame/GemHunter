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

    public IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0.0f;
        Vector3 originPos = transform.localPosition;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originPos;
    }
}

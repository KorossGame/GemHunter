using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuCamera : MonoBehaviour
{
    private CinemachineFreeLook cinemachineCamera;
    private bool rotating;
    private bool activatedDelay;

    public float defaultSpeed = 0.05f;
    public float transitionTime = 10f;

    void Start()
    {
        cinemachineCamera = GetComponent<CinemachineFreeLook>();
        rotating = true;
        activatedDelay = false;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            StopAllCoroutines();
            cinemachineCamera.m_XAxis.m_InputAxisValue = Input.GetAxis("Mouse X");
            rotating = false;
            activatedDelay = false;
        }
        else
        {
            if (rotating)
            {
                activatedDelay = false;
                cinemachineCamera.m_XAxis.m_InputAxisValue = defaultSpeed;
            }
            else if (!activatedDelay)
            {
                cinemachineCamera.m_XAxis.m_InputAxisValue = 0;
                StartCoroutine(AutoRotationDelay());
            }
        }
    }

    private IEnumerator AutoRotationDelay()
    {
        activatedDelay = true;
        yield return new WaitForSeconds(transitionTime);
        rotating = true;
    }
}

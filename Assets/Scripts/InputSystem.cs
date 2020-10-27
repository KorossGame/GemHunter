using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public Player playerPhysicalObject;
    public GameObject playerVisualObject;
    float horizontal, vertical;

    void Update()
    {
        HandleInput();
        HandleRotation();
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    void HandleInput()
    {
        // Movement
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        // Shoot
        if (Input.GetButtonDown("Fire1")) 
            playerPhysicalObject.WeaponEquiped.Shoot(playerVisualObject);
        
        // Gun switch
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            // Next enabled weapon
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            // Previous enabled weapon
        }

        // Reload gun
        if (Input.GetButtonDown("Reload"))
            playerPhysicalObject.WeaponEquiped.Reload();
    }

    void ApplyMovement()
    {
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        movement = Vector3.ClampMagnitude(movement, 1);
        transform.Translate(movement * Time.fixedDeltaTime * playerPhysicalObject.Speed);
    }

    void HandleRotation()
    {
        Plane playerPlane = new Plane(Vector3.up, playerVisualObject.transform.position);
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 1.0f;

        if (playerPlane.Raycast(_ray, out hitDist))
        {
            Vector3 targetPoint = _ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            playerVisualObject.transform.rotation = Quaternion.Slerp(playerVisualObject.transform.rotation, targetRotation, 7f * Time.deltaTime);
        }
    }
}

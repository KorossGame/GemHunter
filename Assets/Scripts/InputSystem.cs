using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public Player playerPhysicalObject;
    public GameObject playerVisualObject;
    private float horizontal, vertical;

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
        if (Input.GetMouseButton(0))
            playerPhysicalObject.inventory.weaponEquiped.Shoot(playerVisualObject);

        SwitchGun();

        // Reload gun
        if (Input.GetButtonDown("Reload"))
            playerPhysicalObject.inventory.weaponEquiped.ForceReload();
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

    void SwitchGun()
    {
        // Gun switch via scroll | ACCESSING INVENTORY OBJECT TRANSFORM SUCKS (new function in inventory class?)
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                // Current selected weapon > childCount-1 (checking if next child exists)
                if (playerPhysicalObject.inventory.selectedWeapon >= playerPhysicalObject.inventory.transform.childCount - 1)
                    playerPhysicalObject.inventory.selectedWeapon = 0;
                else
                    playerPhysicalObject.inventory.selectedWeapon++;
            }
            else
            {
                // Check if current selected weapon is the last?
                if (playerPhysicalObject.inventory.selectedWeapon <= 0)
                    playerPhysicalObject.inventory.selectedWeapon = playerPhysicalObject.inventory.transform.childCount - 1;
                else
                    playerPhysicalObject.inventory.selectedWeapon--;
            }
        }

        // Gun switch via number keys
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            playerPhysicalObject.inventory.selectedWeapon = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && playerPhysicalObject.inventory.transform.childCount - 1 >= 1) {
            playerPhysicalObject.inventory.selectedWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && playerPhysicalObject.inventory.transform.childCount - 1 >= 2) {
            playerPhysicalObject.inventory.selectedWeapon = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && playerPhysicalObject.inventory.transform.childCount - 1 >= 3) {
            playerPhysicalObject.inventory.selectedWeapon = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && playerPhysicalObject.inventory.transform.childCount - 1 >= 4) {
            playerPhysicalObject.inventory.selectedWeapon = 4;
        }

        // Switch if player changed the weapon
        if (playerPhysicalObject.inventory.selectedWeapon!= playerPhysicalObject.inventory.previousWeapon)
        {
            playerPhysicalObject.inventory.SelectWeapon();
        }
    }
}

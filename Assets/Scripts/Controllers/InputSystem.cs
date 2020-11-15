using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Player))]
public class InputSystem : MonoBehaviour
{
    // Physics
    private Player playerPhysicalObject;
    public GameObject playerVisualObject;
    private Rigidbody rb;

    // Input
    private float horizontal, vertical;

    // Movement Boost
    private bool boosted = false;
    private float boostCoefficient = 1.25f;

    // Player
    private Plane playerPlane;

    void Start()
    {
        playerPhysicalObject = GetComponent<Player>();
        rb = GetComponent<Rigidbody>();
        playerPlane = new Plane(Vector3.up, playerVisualObject.transform.position);
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        HandleRotation();
        ApplyMovement();
    }

    void HandleInput()
    {
        // Movement boost
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!boosted)
            {
                playerPhysicalObject.Speed *= boostCoefficient;
                boosted = true;
            }
        }
        else
        {
            if (boosted)
            {
                playerPhysicalObject.Speed /= boostCoefficient;
                boosted = false;
            }
        }

        // Get movement axis value
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // Weapons block
        if (playerPhysicalObject.inventory)
        {
            // If any weapon equipped
            if (playerPhysicalObject.inventory.WeaponEquiped)
            {
                // Shoot
                if (Input.GetMouseButton(0))
                {
                    playerPhysicalObject.inventory.WeaponEquiped.Shoot(playerPhysicalObject);
                }

                // Reload gun
                if (Input.GetButtonDown("Reload"))
                {
                    playerPhysicalObject.inventory.WeaponEquiped.ForceReload();
                }
            }

            // handle input for switching the gun
            SwitchGun();
        }
    }

    void ApplyMovement()
    {
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        movement = Vector3.ClampMagnitude(movement, 1);
        rb.velocity = movement * playerPhysicalObject.Speed;
    }

    void HandleRotation()
    {
        // Ray to mousePosition
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
        int currentWeapon = playerPhysicalObject.inventory.GetCurrentWeaponID();
        int savedWeapon = currentWeapon;

        // Gun switch via scroll
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                currentWeapon++;
            }
            else
            {
                currentWeapon--;
            }
        }

        // Gun switch via number keys
        if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Alpha0))
        {
            currentWeapon = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentWeapon = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentWeapon = 5;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6))
        {
            currentWeapon = 6;
        }

        if (currentWeapon != savedWeapon)
        {
            playerPhysicalObject.inventory.EquipWeapon(currentWeapon);
        }
    }
}

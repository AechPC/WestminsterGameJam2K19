﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTime : MonoBehaviour
{
    private ExorcistMovement movement;
    private ExorcistStamina stamina;
    private ExorcistGun gun;

    private float baseMovementSpeed, baseShootCooldown, baseReloadTime;
    [SerializeField] private float timeScaleTarget, slowSpeed, revertSpeed, revertTolerance, staminaCost;

    [SerializeField] private int minStaminaToActivate;

    private bool slowing;

    private void Start()
    {
        movement = GetComponent<ExorcistMovement>();
        stamina = GetComponent<ExorcistStamina>();
        gun = GetComponent<ExorcistGun>();

        baseMovementSpeed = movement.movementSpeed;
        baseShootCooldown = gun.shootCooldown;
        baseReloadTime = gun.ReloadTime;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) && (stamina.Stamina >= minStaminaToActivate || slowing && stamina.Stamina >= staminaCost))
        {
            slowing = true;
            stamina.Stamina -= staminaCost;
            Time.timeScale = Mathf.Lerp(Time.timeScale, timeScaleTarget, slowSpeed);
            UpdateExorcistAbilities();
        }
        else
        {
            slowing = false;
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1, revertSpeed);
            if (Time.timeScale >= revertTolerance)
            {
                Time.timeScale = 1;
            }

            UpdateExorcistAbilities();
        }
    }

    private void UpdateExorcistAbilities()
    {
        movement.movementSpeed = baseMovementSpeed / Mathf.Pow(Time.timeScale, 2);
        gun.shootCooldown = baseShootCooldown / Time.timeScale;
        gun.ReloadTime = baseReloadTime / Time.timeScale;
    }
}
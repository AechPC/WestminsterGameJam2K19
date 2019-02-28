﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExorcistGun : MonoBehaviour
{
    public float shootCooldown;
    [SerializeField] private float range, reloadTime;
    private float lastShoot, reloadDiff;

    public float ReloadTime
    {
        get { return reloadTime; }
        set
        {
            reloadTime = value;
            reloadDiff = reloadTime - shootCooldown;
        }
    }

    [SerializeField] private int magSize;
    private int bulletsLeft;

    [SerializeField] private Transform gunPos;

    [SerializeField] private LayerMask canHit;

    private void Awake()
    {
        bulletsLeft = magSize;
        reloadDiff = reloadTime - shootCooldown;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > lastShoot + shootCooldown && bulletsLeft > 0)
        {
            lastShoot = Time.time;
            bulletsLeft--;
            RaycastHit2D hit = Physics2D.Raycast(gunPos.transform.position, transform.right, range, canHit);
            IDamageable target = hit.transform?.GetComponent<IDamageable>();
            target?.TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.R)/* && Time.time > lastShoot + shootCooldown*/)
        {
            lastShoot = Time.time + reloadDiff;
            bulletsLeft = magSize;
        }
    }
}
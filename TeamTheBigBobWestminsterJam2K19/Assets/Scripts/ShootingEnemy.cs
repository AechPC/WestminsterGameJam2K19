﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private LayerMask exorcistLayer;

    [SerializeField] private float sightRange, arrowSpeed, shootCooldown;
    private float lastFireTime;

    [SerializeField] private GameObject arrow;

    [SerializeField] private Transform arrowStart;

    [SerializeField] private int health;

    private void Update()
    {
        if (Physics2D.Raycast(transform.position, Vector2.left, sightRange, exorcistLayer)) // Look left
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);

            if (Time.time > lastFireTime + shootCooldown)
            {
                Shoot();
            }

        }
        else if (Physics2D.Raycast(transform.position, Vector2.right, sightRange, exorcistLayer)) // Look right
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

            if (Time.time > lastFireTime + shootCooldown)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        lastFireTime = Time.time;
        Rigidbody2D rb = Instantiate(arrow, arrowStart.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.y < 90 ? 270 : 90)).GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(transform.rotation.eulerAngles.y < 90 ? arrowSpeed : -arrowSpeed, 0);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health < 1)
        {
            Debug.Log("Shooting enemy was DECIMATED!");
            Destroy(gameObject);
        }
    }
}
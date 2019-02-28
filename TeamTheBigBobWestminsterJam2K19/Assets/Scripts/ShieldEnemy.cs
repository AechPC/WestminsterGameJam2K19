using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private LayerMask exorcistLayer;

    [SerializeField] private float sightRange, movementSpeed;

    [SerializeField] private int health, damageDealt;

    private Transform exorcistTransform;

    private Rigidbody2D rb;

    private void Awake()
    {
        exorcistTransform = GameObject.FindWithTag("Exorcist").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Physics2D.Raycast(transform.position, Vector2.left, sightRange, exorcistLayer)) // Look left
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);

            rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
        }
        else if (Physics2D.Raycast(transform.position, Vector2.right, sightRange, exorcistLayer)) // Look right
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

            rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health < 1)
        {
            Debug.Log("Shield guy down");
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Exorcist")
        {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damageDealt);
        }
    }
}
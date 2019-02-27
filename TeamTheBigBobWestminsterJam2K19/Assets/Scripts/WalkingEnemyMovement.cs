using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemyMovement : MonoBehaviour, IDamageable
{
    [SerializeField] private LayerMask exorcistLayer;

    [SerializeField] private int health;

    [SerializeField] private float sightRange, listenRange, movementSpeed;
    private float listenRangeSqr;

    private Transform playerTransform;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        listenRangeSqr = Mathf.Pow(listenRange, 2);
        playerTransform = GameObject.FindWithTag("Exorcist").transform;
    }

    private void Update()
    {
        if (Physics2D.Raycast(transform.position, Vector2.left, sightRange, exorcistLayer) ||   // Look left
            Physics2D.Raycast(transform.position, Vector2.right, sightRange, exorcistLayer) ||  // Look right
            (transform.position - playerTransform.position).sqrMagnitude < listenRangeSqr)      // Listen
        {
            rb.velocity = new Vector2(transform.position.x - playerTransform.position.x < 0 ? movementSpeed : -movementSpeed, rb.velocity.y);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health < 1)
        {
            Debug.Log("Walking enemy was killedead");
            Destroy(gameObject);
        }
    }
}

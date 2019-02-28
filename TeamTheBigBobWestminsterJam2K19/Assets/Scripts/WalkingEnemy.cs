using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private LayerMask exorcistLayer;

    [SerializeField] private int health, damageDealt;

    [SerializeField] private float sightRange, listenRange, movementSpeed;
    private float listenRangeSqr;

    [SerializeField] private Transform patrolPointLeft, patrolPointRight;
    private Transform playerTransform;

    private bool patrollingLeft;

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
        else
        {
            rb.velocity = new Vector2(patrollingLeft ? -movementSpeed : movementSpeed, rb.velocity.y);

            if (patrollingLeft ? transform.position.x < patrolPointLeft.position.x : transform.position.x > patrolPointRight.position.x)
            {
                patrollingLeft = !patrollingLeft;
            }
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

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Exorcist")
        {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damageDealt);
            Debug.Log("Damaged player");
        }
    }
}

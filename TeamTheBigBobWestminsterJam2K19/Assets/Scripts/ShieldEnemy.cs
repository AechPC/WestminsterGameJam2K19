using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : MonoBehaviour, IDamageable, IStunnable
{
    [SerializeField] private LayerMask exorcistLayer;

    [SerializeField] private float sightRange, movementSpeed;

    [SerializeField] private int health, damageDealt;

    private Transform exorcistTransform;

    private bool stunned;

    private Rigidbody2D rb;

    [SerializeField] private Animator anim;

    private void Awake()
    {
        exorcistTransform = GameObject.FindWithTag("Exorcist").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        if (stunned)
        {
            return;
        }

        if (Physics2D.Raycast(transform.position, Vector2.left, sightRange, exorcistLayer)) // Look left
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);

            rb.velocity = new Vector2(-movementSpeed * Time.deltaTime, rb.velocity.y);
        }
        else if (Physics2D.Raycast(transform.position, Vector2.right, sightRange, exorcistLayer)) // Look right
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

            rb.velocity = new Vector2(movementSpeed * Time.deltaTime, rb.velocity.y);
        }
    }

    public void TakeDamage(int damage)
    {
        anim.SetBool("Damage", true);
        health -= damage;

        if (health < 1)
        {
            Debug.Log("Shield guy down");
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Exorcist" && !stunned)
        {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damageDealt);
        }
    }

    public void Stun(float duration)
    {
        stunned = true;
        StartCoroutine(UnStun(duration));
    }

    private IEnumerator UnStun(float duration)
    {
        yield return new WaitForSeconds(duration);
        stunned = false;
    }

    public void StopTakingDamage()
    {
        anim.SetBool("Damage", false);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExorcistMovement : MonoBehaviour
{
    public float movementSpeed;
    [SerializeField] private float raycastDis, jumpForce, velocityLimit;

    [SerializeField] private LayerMask groundLayers;

    private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    [SerializeField] private AudioSource audio;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        bool moved = false;

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("VerticalSpeed", Mathf.Abs(rb.velocity.y));
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moved = true;
            Vector2 movement = new Vector2(-movementSpeed * Time.deltaTime, rb.velocity.y);
            transform.rotation = Quaternion.Euler(0, 180, 0);

            rb.velocity = movement;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moved = true;
            Vector2 movement = new Vector2(movementSpeed * Time.deltaTime, rb.velocity.y);
            transform.rotation = Quaternion.Euler(0, 0, 0);

            rb.velocity = movement;
        }
        else
        {

            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDis, groundLayers); // For testing distance to ground for Raycast
        //Debug.Log(hit.distance);

        if (Physics2D.Raycast(transform.position, Vector2.down, raycastDis, groundLayers))
        {
            if (moved && !audio.isPlaying)
            {
                audio.Play();
            }

            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
            {
                rb.AddForce(new Vector2(0, jumpForce));
            }

        }

        if (rb.velocity.magnitude > velocityLimit)
        {
            rb.velocity = rb.velocity.normalized * velocityLimit;
        }
    }
}
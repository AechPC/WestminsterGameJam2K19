using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExorcistMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed, raycastDis, jumpForce;

    [SerializeField] private LayerMask groundLayers;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

	private void Update () {
	    //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDis, groundLayers); // For testing distance to ground for Raycast
        //Debug.Log(hit.distance);

	    if (Physics2D.Raycast(transform.position, Vector2.down, raycastDis, groundLayers) && Input.GetKeyDown(KeyCode.W))
	    {
	        rb.AddForce(new Vector2(0, jumpForce));
	    }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Vector2 movement = new Vector2(-movementSpeed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(0, 180, 0);


            rb.velocity = movement;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Vector2 movement = new Vector2(movementSpeed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(0, 0, 0);

            rb.velocity = movement;
        }
    }
}

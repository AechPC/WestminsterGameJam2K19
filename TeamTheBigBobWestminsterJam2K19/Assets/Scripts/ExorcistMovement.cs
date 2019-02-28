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
	
	// Update is called once per frame
	void Update () {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal") * movementSpeed, rb.velocity.y);
	    if (movement.x != 0)
	        transform.rotation = Quaternion.Euler(0, movement.x > 0 ? 0 : 180, 0);
        rb.velocity = movement;

	    //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDis, groundLayers);
     //   Debug.Log(hit.distance);

	    if (Physics2D.Raycast(transform.position, Vector2.down, raycastDis, groundLayers) && Input.GetKeyDown(KeyCode.W))
	    {
	        rb.AddForce(new Vector2(0, jumpForce));
	    }
    }
}

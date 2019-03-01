using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{

    [SerializeField] private float movementSpeed;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	private void FixedUpdate ()
	{
	    Vector2 movement = new Vector2(Input.GetAxis("ControllerHorizontal"), Input.GetAxis("ControllerVertical")) * movementSpeed;
	    rb.velocity = movement;
	    //transform.Translate(movement);
	}
}

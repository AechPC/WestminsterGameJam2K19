using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{

    [SerializeField] private float movementSpeed;
	
	// Update is called once per frame
	private void FixedUpdate ()
	{
	    Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * movementSpeed;
	    transform.Translate(movement);
	}
}

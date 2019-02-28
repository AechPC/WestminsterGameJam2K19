using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundries : MonoBehaviour {
	private Vector2 screenbound;
	private float objectHeight;
	private float objectWidth;
	// Use this for initialization
	void Start () {
		screenbound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
		objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
		objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		Vector3 viewPositon = transform.position;
		screenbound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
		viewPositon.x = Mathf.Clamp(viewPositon.x, screenbound.x -17.5f + objectWidth, screenbound.x - objectWidth -0.5f);
		viewPositon.y = Mathf.Clamp(viewPositon.y, screenbound.y -10 + objectHeight, screenbound.y - objectHeight - 0.5f);
		transform.position = viewPositon;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainCamera : MonoBehaviour {

	[SerializeField]
	private float maximumX;
	[SerializeField]
	private float maximumY;
	[SerializeField]
	private float minimumX;
	[SerializeField]
	private float minimumY;

	private Transform target;

	// Use this for initialization
	void Start () {
		target = GameObject.FindWithTag("Exorcist").transform;
	
	}

	// Update is called once per frame
	void Update() {
		transform.position = new Vector3(target.position.x, target.position.y + 3.3f, -10);
	}
}

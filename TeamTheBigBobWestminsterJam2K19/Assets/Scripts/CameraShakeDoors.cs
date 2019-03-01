using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeDoors : MonoBehaviour {

	public Camera ShakeCamera;

	private PlayerMainCamera cam;

	float shakeAmount = 0;

    private Vector3 standardOffset;

	private void Awake()
	{
		cam = GetComponent<PlayerMainCamera>();
	}

    private void Start()
    {
        standardOffset = cam.offset;
    }

	void check()
	{
		if (ShakeCamera == null)
		{
			ShakeCamera = Camera.main;
		}
	}

	public void Shake(float amount, float length)
	{
		Debug.Log("Starting shake");
		shakeAmount = amount;
		InvokeRepeating("startShaking", 0, 0.01f);
		Invoke("stopShaking", length);
	}

	void startShaking()
	{
		Debug.Log("SHAKING!");
		if(shakeAmount > 0)
		{
			Vector3 camPosition/* = ShakeCamera.transform.position*/ = new Vector3();
			float offSetAmountX = Random.value * shakeAmount * 2 - shakeAmount;
			float offSetAmountY = Random.value * shakeAmount * 2 - shakeAmount;
			camPosition.x += offSetAmountX;
			camPosition.y += offSetAmountY;
			cam.offset = camPosition + standardOffset;
			//ShakeCamera.transform.position = camPosition; 
		}
	}

	void stopShaking()
	{
		Debug.Log("Stopping shak");
		CancelInvoke("startShaking");
		cam.offset = standardOffset;
		//ShakeCamera.transform.position = Vector3.;
	}


	//// Update is called once per frame
	//private void Update ()
	//{
	//	Debug.Log("Updating");
	//	if (Input.GetKeyDown(KeyCode.F))
	//	{
	//		Debug.Log("Calling shake");
	//		Shake(10.0f, 0.5f);
	//	}
	//}
}

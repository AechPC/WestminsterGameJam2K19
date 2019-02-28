using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GhostPossess : MonoBehaviour {

    [SerializeField] private float maxPlatformPossessDis, platformMovementSpeed;
    private float platformPossessDisSqr;

    private Platform[] platforms;
    private Platform possessingPlatform;

    [SerializeField] private Image possessIndicator;

    private GhostMovement movement;

    private Renderer rend;
    private Collider2D coll;

    private bool possessing;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        coll = GetComponent<Collider2D>();
        movement = GetComponent<GhostMovement>();
        platforms = GameObject.FindGameObjectsWithTag("Platform").Select(o => o.GetComponent<Platform>()).ToArray();
        platformPossessDisSqr = Mathf.Pow(maxPlatformPossessDis, 2);
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (!possessing)
	    {
	        Platform possessablePlatform = GetPossessablePlatform();
	        possessIndicator.enabled = possessablePlatform;

	        if (possessablePlatform && Input.GetKeyDown(KeyCode.P))
	        {
                Possess(possessablePlatform);
	        }
        }
	    else
	    {
	        PossessUpdate();
	    }
	}

    private void Possess(Platform platform)
    {
        possessingPlatform = platform;
        possessing = true;
        rend.enabled = false;
        coll.enabled = false;
        movement.enabled = false;
        possessIndicator.enabled = false;
    }

    private void UnPossess()
    {
        transform.position = possessingPlatform.transform.position + new Vector3(0, 1);
        possessingPlatform = null;
        possessing = false;
        rend.enabled = true;
        coll.enabled = true;
        movement.enabled = true;
    }

    private void PossessUpdate()
    {
        possessingPlatform.Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * platformMovementSpeed);

        if (Input.GetKeyDown(KeyCode.P))
        {
            UnPossess();
        }
    }

    private Platform GetPossessablePlatform()
    {
        Platform possessablePlatform = null;
        float minDisSqr = Mathf.Infinity;

        // Get closest platform
        foreach (Platform platform in platforms)
        {
            float disSqr = (transform.position - platform.transform.position).sqrMagnitude;
            if (disSqr < minDisSqr)
            {
                possessablePlatform = platform;
                minDisSqr = disSqr;
            }
        }

        return minDisSqr <= platformPossessDisSqr ? possessablePlatform : null;
    }
}

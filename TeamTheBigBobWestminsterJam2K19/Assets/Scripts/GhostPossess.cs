using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GhostPossess : MonoBehaviour {

    [SerializeField] private float maxPlatformPossessDis, platformMovementSpeed;
    private float platformPossessDisSqr;

    private Platform[] platforms;
    private Platform possessingPlatform, toPossess;

    [SerializeField] private Image possessIndicator;

    private GhostMovement movement;

    private Renderer rend;
    private Collider2D coll;

    private bool possessing, hasResetAButton, hasResetXButton;

    [SerializeField] private Animator anim;

    private AudioSource audio;

    [SerializeField] private AudioClip shockwaveSFX;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        coll = GetComponent<Collider2D>();
        movement = GetComponent<GhostMovement>();
        platforms = GameObject.FindGameObjectsWithTag("Platform").Select(o => o.GetComponent<Platform>()).ToArray();
        platformPossessDisSqr = Mathf.Pow(maxPlatformPossessDis, 2);
    }
	
	// Update is called once per frame
	private void Update ()
	{
	    if (Input.GetAxis("ControllerA") == 0)
	    {
	        hasResetAButton = true;
	    }

        if (!possessing)
	    {
	        Platform possessablePlatform = GetPossessablePlatform();
	        possessIndicator.enabled = possessablePlatform;

	        if (possessablePlatform && Input.GetAxis("ControllerA") > 0 && hasResetAButton)
	        {
	            hasResetAButton = false;
	            toPossess = possessablePlatform;
	            anim.SetBool("Possess", true);
	            audio.clip = shockwaveSFX;
                audio.Play();
	        }
        }
	    else
	    {
	        PossessUpdate();
	    }
	}

    public void Possess()
    {
        if (!anim.GetBool("Possess"))
        {
            return;
        }

        possessingPlatform = toPossess;
        possessing = true;
        rend.enabled = false;
        coll.enabled = false;
        movement.enabled = false;
        possessIndicator.enabled = false;
    }

    private void UnPossess()
    {
        anim.SetBool("Possess", false);
        transform.position = possessingPlatform.transform.position + new Vector3(0, 2);
        possessingPlatform = null;
        possessing = false;
        rend.enabled = true;
        coll.enabled = true;
        movement.enabled = true;
    }

    private void PossessUpdate()
    {
        possessingPlatform.Move(new Vector2(Input.GetAxis("ControllerHorizontal"), Input.GetAxis("ControllerVertical")) * platformMovementSpeed);

        if (Input.GetAxis("ControllerA") > 0 && hasResetAButton)
        {
            hasResetAButton = false;
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

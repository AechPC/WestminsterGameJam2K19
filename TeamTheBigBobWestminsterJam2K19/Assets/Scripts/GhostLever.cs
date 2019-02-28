using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostLever : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lever")
        {
            Debug.Log("Colliding");
            collision.gameObject.GetComponent<LeverSwitch>().ActivateLever();
        }
    }
}
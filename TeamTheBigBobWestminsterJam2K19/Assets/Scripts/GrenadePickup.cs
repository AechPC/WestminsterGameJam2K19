using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickup : MonoBehaviour
{
    [SerializeField] private int grenadeAmount;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Exorcist")
        {
            other.gameObject.GetComponent<ExorcistGrenade>().GetGrenades(grenadeAmount);
            Destroy(gameObject);
        }
    }
}
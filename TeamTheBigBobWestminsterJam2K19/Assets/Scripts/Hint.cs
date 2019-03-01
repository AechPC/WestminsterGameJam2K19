using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    [SerializeField] private GameObject hint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Exorcist")
        {
            hint.SetActive(true);   
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Exorcist")
        {
            hint.SetActive(false);
        }
    }
}
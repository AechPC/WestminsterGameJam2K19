using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverSwitch : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private Sprite open;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void ActivateLever()
    {
        if (sr.sprite != open)
        {
            sr.sprite = open;
            door.GetComponent<Door>().OpenDoor();
        }
    }
}
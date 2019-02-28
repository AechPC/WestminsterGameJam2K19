using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Collider2D collisionBox;

    private void Start()
    {
        collisionBox = GetComponent<BoxCollider2D>();
    }

    public void OpenDoor()
    {
        collisionBox.isTrigger = true;
    }
}
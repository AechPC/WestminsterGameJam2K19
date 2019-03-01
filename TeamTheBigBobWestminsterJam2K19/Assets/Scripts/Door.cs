using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Collider2D collisionBox;

    [SerializeField] private Sprite open;

    private void Start()
    {
        collisionBox = GetComponent<BoxCollider2D>();
    }

    public void OpenDoor()
    {
        collisionBox.isTrigger = true;
        GetComponent<SpriteRenderer>().sprite = open;
    }
}
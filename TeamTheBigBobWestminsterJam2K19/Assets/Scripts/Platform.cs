using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Axis
{
    Horizontal,
    Vertical
}

public class Platform : MonoBehaviour
{
    [SerializeField] private Axis axis;

    private Collider2D coll;

    [SerializeField, Tooltip("If horizontal axis, then clamp 1 is left and clamp 2 is right. If vertical axis, then clamp 1 is top and clamp 2 is bottom")]
    private Transform clamp1, clamp2;

    private bool hasClamped;

    [SerializeField] private float exorcistMovementMultiplier;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    public void Move(Vector2 movement)
    {
        Vector2 translation = axis == Axis.Horizontal ? new Vector2(movement.x, 0) : new Vector2(0, movement.y);

        hasClamped = (axis == Axis.Horizontal && (clamp1.position.x > (transform.position.x + translation.x) ||
                      clamp2.position.x < (transform.position.x + translation.x)));
        
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x + translation.x, 
            axis == Axis.Horizontal ? clamp1.position.x : transform.position.x, 
            axis == Axis.Horizontal ? clamp2.position.x : transform.position.x), 
            Mathf.Clamp(transform.position.y + translation.y,
            axis == Axis.Horizontal ? transform.position.y : clamp2.position.y,
            axis == Axis.Horizontal ? transform.position.y : clamp1.position.y));
        

        Collider2D[] contacts = new Collider2D[5];
        if (axis == Axis.Horizontal && coll.GetContacts(contacts) > 0 && !hasClamped)
        {
            foreach (Collider2D coll in contacts)
            {
                if (coll && coll.tag == "Exorcist")
                {
                    coll.transform.position = new Vector3(coll.transform.position.x + (translation.x * exorcistMovementMultiplier), coll.transform.position.y);
                }
            }
        }
    }
}
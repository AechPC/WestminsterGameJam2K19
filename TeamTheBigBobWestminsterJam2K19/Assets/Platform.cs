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

    public void Move(Vector2 movement)
    {
        Vector2 translation = axis == Axis.Horizontal ? new Vector2(movement.x, 0) : new Vector2(0, movement.y);
        transform.Translate(translation);
    }
}
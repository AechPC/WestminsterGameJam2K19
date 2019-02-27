using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public void Move(Vector2 movement)
    {
        transform.Translate(movement);
    }
}
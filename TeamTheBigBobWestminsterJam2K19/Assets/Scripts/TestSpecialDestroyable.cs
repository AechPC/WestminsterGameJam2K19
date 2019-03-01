using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpecialDestroyable : MonoBehaviour, ISpecialDestroy
{
    public void SpecialDestroy()
    {
        Destroy(gameObject);
    }
}
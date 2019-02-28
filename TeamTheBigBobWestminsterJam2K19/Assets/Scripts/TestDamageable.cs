using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamageable : MonoBehaviour, IDamageable
{
    public void TakeDamage(int damage)
    {
        Debug.Log("I took " + damage + " points of ouch!");
    }
}

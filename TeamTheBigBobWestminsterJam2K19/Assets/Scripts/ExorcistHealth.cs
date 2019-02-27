using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExorcistHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;
    private int health;

    private void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health < 1)
        {
            Debug.Log("You dead son. Do the game over thing");
        }
    }
}

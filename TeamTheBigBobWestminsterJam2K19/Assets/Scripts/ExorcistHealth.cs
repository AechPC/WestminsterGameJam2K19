using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExorcistHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;
    private int health;

    [SerializeField] private float invulnerabilityTime;
    private float lastDamageTime;

    private void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (Time.time > lastDamageTime + invulnerabilityTime)
        {
            lastDamageTime = Time.time;
            health -= damage;

            if (health < 1)
            {
                Debug.Log("You dead son. Do the game over thing");
            }
        }
    }
}

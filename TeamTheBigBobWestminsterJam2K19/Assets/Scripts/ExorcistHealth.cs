using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExorcistHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;
    private int health;

    [SerializeField] private float invulnerabilityTime;
    private float lastDamageTime;

    [SerializeField] private Image healthBar;

    [HideInInspector] public bool immune;

    private void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (immune)
        {
            return;
        }

        if (Time.time > lastDamageTime + invulnerabilityTime)
        {
            lastDamageTime = Time.time;
            health -= damage;

            healthBar.fillAmount = (float)health / maxHealth;

            if (health < 1)
            {
                Debug.Log("You dead son. Do the game over thing");
            }
        }
    }
}

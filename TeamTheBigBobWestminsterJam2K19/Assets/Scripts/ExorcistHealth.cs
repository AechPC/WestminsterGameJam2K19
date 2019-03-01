using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExorcistHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth, healthPickupHealing;
    private int health;

    [SerializeField] private float invulnerabilityTime;
    private float lastDamageTime;

    [SerializeField] private Image healthBar;

    [HideInInspector] public bool immune;

    [SerializeField] private Animator anim;

    private AudioSource audio;

    [SerializeField] private AudioClip hitSFX;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
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
            audio.clip = hitSFX;
            audio.Play();
            anim.SetBool("Damage", true);
            lastDamageTime = Time.time;
            health -= damage;

            healthBar.fillAmount = (float)health / maxHealth;

            if (health < 1)
            {
                Debug.Log("You dead son. Do the game over thing");
                GameOver();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "UnholyWater" && !immune)
        {
            Debug.Log("Exorcist diedead. Game over should ensue here");
            GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "HealthPickup")
        {
            Destroy(other.gameObject);
            health = Mathf.Clamp(health + healthPickupHealing, 0, maxHealth);
            healthBar.fillAmount = (float)health / maxHealth;
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene(4);
    }

    public void StopTakingDamage()
    {
        Debug.Log("Stopped taking damage");
        anim.SetBool("Damage", false);
    }
}
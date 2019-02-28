using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExorcistHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;
    private int health;

    [SerializeField] private float invulnerabilityTime, damageTakenDuration;
    private float lastDamageTime;

    [SerializeField] private Image healthBar;

    [HideInInspector] public bool immune;

    [SerializeField] private Animator anim;

    private SpriteRenderer rend;

    [SerializeField] private Sprite damageTaken;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (immune)
        {
            return;
        }

        anim.SetBool("Damage", true);
        //rend.sprite = damageTaken;
        //StartCoroutine(StopTakingDamage());

        if (Time.time > lastDamageTime + invulnerabilityTime)
        {
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

    private void GameOver()
    {
        SceneManager.LoadScene(4);
    }

    public void StopTakingDamage()
    {
        Debug.Log("Stopped taking damage");
        anim.SetBool("Damage", false);
    }

    //private IEnumerator StopTakingDamage()
    //{
    //    yield return new WaitForSeconds(damageTakenDuration);
    //    Debug.Log("Stopped taking damage");
    //    anim.SetBool("Damage", false);
    //}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private int damageDealt;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Exorcist")
        {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damageDealt);
        }
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExorcistShockwave : MonoBehaviour
{
    [SerializeField] private float cooldown;
    private float lastUsed;

    [SerializeField] private int damage;

    [SerializeField] private Vector2 dimensions;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && Time.time > lastUsed + cooldown)
        {
            lastUsed = Time.time;
            Vector2 lowerCorner = new Vector2(transform.position.x + (transform.rotation.eulerAngles.y < 90 ? transform.localScale.x: -transform.localScale.x), transform.position.y - transform.localScale.y / 2);
            Collider2D[] hit = Physics2D.OverlapAreaAll(lowerCorner, lowerCorner + new Vector2(transform.rotation.eulerAngles.y < 90 ? dimensions.x : -dimensions.x, dimensions.y));
            foreach (Collider2D coll in hit)
            {
                IDamageable damageable = coll.GetComponent<IDamageable>();
                ISpecialDestroy specialDestroy = coll.GetComponent<ISpecialDestroy>();
                damageable?.TakeDamage(damage);
                specialDestroy?.SpecialDestroy();
            }
        }
    }
}

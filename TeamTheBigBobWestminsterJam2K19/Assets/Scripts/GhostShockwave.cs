using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GhostShockwave : MonoBehaviour
{
    [SerializeField] private float cooldown, radius, pushForce;
    private float lastUse;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && Time.time > lastUse + cooldown)
        {
            Debug.Log("BLASTING!");
            lastUse = Time.time;

            Rigidbody2D[] inRange = Physics2D.OverlapCircleAll(transform.position, radius).Select(c => c.GetComponent<Rigidbody2D>()).ToArray();
            foreach (Rigidbody2D rigidbody in inRange)
            {
                if (rigidbody)
                {
                    Vector2 direction = (rigidbody.transform.position - transform.position).normalized;
                    rigidbody.AddForce(direction * (radius - Vector2.Distance(transform.position, rigidbody.transform.position)) * pushForce);
                }
            }
        }
    }
}

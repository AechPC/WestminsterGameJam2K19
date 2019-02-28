using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GhostShockwave : MonoBehaviour
{
    [SerializeField] private float cooldown, radius, pushForce, stunDuration;
    private float lastUse;

    private bool hasResetXButton;

    private void Update()
    {
        if (Input.GetAxis("ControllerX") == 0)
        {
            hasResetXButton = true;
        }

        if (Input.GetAxis("ControllerX") > 0 && Time.time > lastUse + cooldown)
        {
            hasResetXButton = false;
            Debug.Log("BLASTING!");
            lastUse = Time.time;

            Collider2D[] inRange = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D coll in inRange)
            {
                coll.GetComponent<SpecialPushable>()?.EnableMovement();

                Rigidbody2D rb = coll.GetComponent<Rigidbody2D>();
                if (rb)
                {
                    Vector2 direction = (rb.transform.position - transform.position).normalized;
                    rb.AddForce(direction * (radius - Vector2.Distance(transform.position, rb.transform.position)) * pushForce);
                }

                coll.GetComponent<IStunnable>()?.Stun(stunDuration);
            }
        }
    }
}

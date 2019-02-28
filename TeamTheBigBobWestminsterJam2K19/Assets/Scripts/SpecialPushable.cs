using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPushable : MonoBehaviour
{
    [SerializeField] private float movementDuration;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void EnableMovement()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        StartCoroutine(DisableMovement());
    }

    private IEnumerator DisableMovement()
    {
        yield return new WaitForSeconds(movementDuration);

        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}

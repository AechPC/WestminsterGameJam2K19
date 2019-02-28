using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HolyHandGrenade : MonoBehaviour
{
    [SerializeField] private int damage;

    [SerializeField] private float fuseTime, range;
    private float startTime;

    private void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (Time.time > startTime + fuseTime)
        {
            IDamageable[] hit = Physics2D.OverlapCircleAll(transform.position, range).Select(c => c.transform.GetComponent<IDamageable>()).ToArray();

            foreach (IDamageable damageable in hit)
            {
                damageable?.TakeDamage(damage);
            }

            Debug.Log("BOOM! Holyness overcomes you");
            Destroy(gameObject);
        }
    }
}

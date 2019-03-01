using System.Collections;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour, IDamageable, IStunnable
{
    [SerializeField] private LayerMask sightLayers;

    [SerializeField] private int health, damageDealt;

    [SerializeField] private float sightRange, listenRange, movementSpeed;
    private float listenRangeSqr;

    [SerializeField] private Transform patrolPointLeft, patrolPointRight;
    private Transform playerTransform;

    private bool patrollingLeft, stunned;

    private Rigidbody2D rb;

    [SerializeField] private Animator anim;

    private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        listenRangeSqr = Mathf.Pow(listenRange, 2);
        playerTransform = GameObject.FindWithTag("Exorcist").transform;
    }

    private void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        if (stunned)
        {
            return;
        }

        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, sightRange, sightLayers);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, sightRange, sightLayers);

        if (hitRight.transform && hitRight.transform.tag == "Exorcist")
        {
            rb.velocity = new Vector2((transform.position.x - playerTransform.position.x < 0 ? movementSpeed : -movementSpeed) * Time.deltaTime, rb.velocity.y);
        }
        else if (hitLeft.transform && hitLeft.transform.tag == "Exorcist")
        {
            rb.velocity = new Vector2((transform.position.x - playerTransform.position.x < 0 ? movementSpeed : -movementSpeed) * Time.deltaTime, rb.velocity.y);
        }
        else if ((transform.position - playerTransform.position).sqrMagnitude < listenRangeSqr) // Listen
        {
            rb.velocity = new Vector2((transform.position.x - playerTransform.position.x < 0 ? movementSpeed : -movementSpeed) * Time.deltaTime, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2((patrollingLeft ? -movementSpeed : movementSpeed) * Time.deltaTime, rb.velocity.y);

            if (patrollingLeft ? transform.position.x < patrolPointLeft.position.x : transform.position.x > patrolPointRight.position.x)
            {
                patrollingLeft = !patrollingLeft;
            }
        }

        if (Mathf.Abs(rb.velocity.x) > 0.01f)
        {
            transform.rotation = Quaternion.Euler(0, rb.velocity.x > 0 ? 0 : 180, 0);
        }
    }

    public void TakeDamage(int damage)
    {
        anim.SetBool("Damage", true);
        health -= damage;
        audio.Play();

        if (health < 1)
        {
            Debug.Log("Walking enemy was killedead");
            anim.SetTrigger("Dead");
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Exorcist" && !stunned)
        {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damageDealt);
            Debug.Log("Damaged player");
        }
    }

    public void Stun(float duration)
    {
        stunned = true;
        StopAllCoroutines();
        StartCoroutine(UnStun(duration));
    }

    private IEnumerator UnStun(float duration)
    {
        yield return new WaitForSeconds(duration);
        stunned = false;
    }

    public void StopTakingDamage()
    {
        anim.SetBool("Damage", false);
    }

    public void Dead()
    {
        Destroy(gameObject);
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GhostShockwave : MonoBehaviour
{
    [SerializeField] private float cooldown, radius, pushForce, stunDuration;
    private float lastUse;

    public bool upgraded;
    private bool hasResetXButton;

    [SerializeField] private Animator abilityAnim, anim;

    private AudioSource audio;

    [SerializeField] private AudioClip shockwaveSFX;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetAxis("ControllerX") == 0)
        {
            hasResetXButton = true;
        }

        if (Input.GetAxis("ControllerX") > 0 && Time.time > lastUse + cooldown)
        {

            abilityAnim.SetTrigger(upgraded ? "UnholyII" : "Unholy");
            audio.clip = shockwaveSFX;
            audio.Play();
            anim.SetTrigger("Shockwave");

            hasResetXButton = false;
            Debug.Log("BLASTING!");
            lastUse = Time.time;

            Collider2D[] inRange = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D coll in inRange)
            {
                if (upgraded)
                {
                    coll.GetComponent<SpecialPushable>()?.EnableMovement();
                    coll.GetComponent<IStunnable>()?.Stun(stunDuration);
                }

                Rigidbody2D rb = coll.GetComponent<Rigidbody2D>();
                if (rb)
                {
                    Vector2 direction = (rb.transform.position - transform.position).normalized;
                    rb.AddForce(direction * (radius - Vector2.Distance(transform.position, rb.transform.position)) * pushForce);
                }
            }
        }
    }
}

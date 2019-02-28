using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExorcistShield : MonoBehaviour
{
    [SerializeField] private float duration;

    private ExorcistHealth health;
    private ExorcistStamina stamina;
    private ExorcistGun gun;
    private ExorcistGrenade grenade;

    private void Awake()
    {
        health = GetComponent<ExorcistHealth>();
        stamina = GetComponent<ExorcistStamina>();
        gun = GetComponent<ExorcistGun>();
        grenade = GetComponent<ExorcistGrenade>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && stamina.Stamina == stamina.maxStamina)
        {
            stamina.Stamina = 0;
            EnableShield();
            StartCoroutine(DisableShield(duration));
        }
    }

    private void EnableShield()
    {
        Debug.Log("Enabling shield");
        health.immune = true;
        gun.canShoot = false;
        grenade.canThrow = false;
    }

    private IEnumerator DisableShield(float delay)
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("Disabling shield");
        health.immune = false;
        gun.canShoot = true;
        grenade.canThrow = true;
    }
}
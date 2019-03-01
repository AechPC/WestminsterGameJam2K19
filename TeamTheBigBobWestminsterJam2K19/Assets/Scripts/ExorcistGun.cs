using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExorcistGun : MonoBehaviour
{
    public float shootCooldown;
    [SerializeField] private float range, reloadTime, cameraShakeIntensity, cameraShakeDuration;
    private float lastShoot, reloadDiff;

    [HideInInspector] public bool canShoot;

    [SerializeField] private Animator anim;

    private AudioSource audio;

    [SerializeField] private AudioClip shootSFX;

    private bool reloading;

    public float ReloadTime
    {
        get { return reloadTime; }
        set
        {
            reloadTime = value;
            reloadDiff = reloadTime - shootCooldown;
        }
    }

    [SerializeField] private int magSize;
    private int bulletsLeft;

    [SerializeField] private Transform gunPos;

    [SerializeField] private LayerMask canHit;

    [SerializeField] private Image reloadImage;

    [SerializeField] private Text ammoText;

    private CameraShakeDoors cameraShake;

    private void Awake()
    {
        cameraShake = GameObject.FindWithTag("MainCamera").GetComponent<CameraShakeDoors>();
        audio = GetComponent<AudioSource>();
        canShoot = true;
        bulletsLeft = magSize;
        reloadDiff = reloadTime - shootCooldown;
    }

    private void Update()
    {
        if (Time.time > lastShoot + shootCooldown && bulletsLeft > 0 && canShoot)
        {
            reloadImage.fillAmount = 0;
            reloading = false;
            ammoText.text = "Ammo: " + bulletsLeft + "/" + magSize;
        }
        else if (reloading)
        {
            reloadImage.fillAmount = 1 - ((lastShoot + shootCooldown) - Time.time) / reloadTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > lastShoot + shootCooldown && bulletsLeft > 0 && canShoot)
        {
            cameraShake.Shake(cameraShakeIntensity, cameraShakeDuration);
            anim.SetBool("Shooting", true);
            audio.clip = shootSFX;
            audio.Play();

            lastShoot = Time.time;
            bulletsLeft--;
            ammoText.text = "Ammo: " + bulletsLeft + "/" + magSize;
            RaycastHit2D hit = Physics2D.Raycast(gunPos.transform.position, transform.right, range, canHit);
            IDamageable target = hit.transform?.GetComponent<IDamageable>();
            target?.TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.R)/* && Time.time > lastShoot + shootCooldown*/)
        {
            reloading = true;
            lastShoot = Time.time + reloadDiff;
            bulletsLeft = magSize;
        }
    }

    public void StopShooting()
    {
        anim.SetBool("Shooting", false);
    }
}
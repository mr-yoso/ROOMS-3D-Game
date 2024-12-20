using UnityEngine;
using System.Collections;
using TMPro;
using System;
using Unity.VisualScripting;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 7f;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject impactEffectBlood;

    public AudioClip gunshotSound; // Unique gunshot sound for this weapon

    private float nextTimeToFire = 0f;
    AudioManager audioManager;

    private bool isDamageBoostActive = false; // Track if the damage boost is active
    private float originalDamage;            // Store the original damage value

    public Animator gunAnimator;

    [Header("UI Elements")]
    public TextMeshProUGUI powerStatusText; // Reference to the power-up status text for 
    public TextMeshProUGUI ammoText;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        originalDamage = damage; // Store the original damage value

        // Initialize the power-up text to be empty
        if (powerStatusText != null)
        {
            powerStatusText.text = "";
        }
    }

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    public void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = $"{currentAmmo:000}";
        }
    }

    void Update()
    {
        if (isReloading)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void OnEnable()
    {
        isReloading = false;
        gunAnimator.SetBool("Reloading", false);

        UpdateAmmoUI();
    }

    IEnumerator Reload()
    {
        isReloading = true;

        gunAnimator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - .25f);

        gunAnimator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        UpdateAmmoUI();
        
        isReloading = false;
    }

    void Shoot()
    {
        muzzleFlash.Play();
        if (gunshotSound != null)
        {
            audioManager.PlaySFX(gunshotSound);
        }

        currentAmmo--;
        UpdateAmmoUI();

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            AIController ai = hit.transform.GetComponent<AIController>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            if (ai != null)
            {
                ai.TakeDamage(damage);
            }

            if (hit.transform.CompareTag("Enemy"))
            {
                GameObject impactBloodGO = Instantiate(impactEffectBlood, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactBloodGO, 0.5f);
            }
            else
            {
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }

            // Enemy enemy = hit.transform.GetComponent<Enemy>();
            // if (enemy != null)
            // {
            //     enemy.TakeDamage(damage);
            // }
        }
    }

    public void ApplyDamageBoost(float multiplier, float duration)
    {
        if (!isDamageBoostActive)
        {
            StartCoroutine(DamageBoostCoroutine(multiplier, duration));
        }
    }

    private IEnumerator DamageBoostCoroutine(float multiplier, float duration)
    {
        isDamageBoostActive = true;
        damage *= multiplier; // Increase damage by multiplier

        // Update the UI text to show the damage boost is active
        if (powerStatusText != null)
        {
            powerStatusText.text = "Damage Boost Active!";
        }

        Debug.Log($"Damage Boost Activated! New Damage: {damage}");

        yield return new WaitForSeconds(duration);

        // Reset damage to original value
        damage = originalDamage;
        isDamageBoostActive = false;

        // Reset the UI text
        if (powerStatusText != null)
        {
            powerStatusText.text = "";
        }

        Debug.Log("Damage Boost Ended!");
    }
}

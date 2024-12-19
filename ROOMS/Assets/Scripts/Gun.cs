using UnityEngine;
using System.Collections; // Add this to fix the error

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 7f;

    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject impactEffectBlood;


    private float nextTimeToFire = 0f;
    AudioManager audioManager;

    private bool isDamageBoostActive = false; // Track if the damage boost is active
    private float originalDamage;            // Store the original damage value

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        originalDamage = damage; // Store the original damage value
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        audioManager.PlaySFX(audioManager.gunshot);

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

        Debug.Log($"Damage Boost Activated! New Damage: {damage}");

        yield return new WaitForSeconds(duration);

        // Reset damage to original value
        damage = originalDamage;
        isDamageBoostActive = false;

        Debug.Log("Damage Boost Ended!");
    }
}

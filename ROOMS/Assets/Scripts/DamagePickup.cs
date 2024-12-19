using UnityEngine;

public class DamagePickup : MonoBehaviour
{
    [SerializeField] private float damageMultiplier = 2f; // Multiplier for gun damage
    [SerializeField] private float duration = 5f;         // Duration of the boost
    [SerializeField] private AudioClip pickupSound;       // Optional: Sound effect for pickup

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Find the Gun script on the player
            Gun gun = other.GetComponentInChildren<Gun>();
            if (gun != null)
            {
                gun.ApplyDamageBoost(damageMultiplier, duration);
            }

            // Optional: Play sound effect
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null && pickupSound != null)
            {
                audioManager.PlaySFX(pickupSound);
            }

            // Destroy the power-up object
            Destroy(gameObject);
        }
    }
}

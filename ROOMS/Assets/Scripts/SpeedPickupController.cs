using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 2f; // Multiplier for speed boost
    [SerializeField] private float duration = 10f;
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.ApplySpeedBoost(speedMultiplier, duration);

                // Update the PowerHUD to show the speed boost
                PowerHUD powerHUD = FindObjectOfType<PowerHUD>();
                if (powerHUD != null)
                {
                    powerHUD.ShowPowerStatus("Speed", true);

                    // Hide the power status after the boost ends
                    Invoke(nameof(HidePowerStatus), duration);
                }
            }

            // Play the pickup sound using AudioManager
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null && pickupSound != null)
            {
                audioManager.PlaySFX(pickupSound);
            }

            // Destroy the speed pickup
            Destroy(gameObject);
        }
    }

    private void HidePowerStatus()
    {
        PowerHUD powerHUD = FindObjectOfType<PowerHUD>();
        if (powerHUD != null)
        {
            powerHUD.ShowPowerStatus("Speed", false);
        }
    }
}

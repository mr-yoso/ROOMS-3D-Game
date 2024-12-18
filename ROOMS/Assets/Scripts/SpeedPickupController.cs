using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 2f; // Multiplier for speed boost
    [SerializeField] private float duration = 10f;        // Duration of the boost

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.ApplySpeedBoost(speedMultiplier, duration);
            }

            // Destroy the speed pickup
            Destroy(gameObject);
        }
    }
}

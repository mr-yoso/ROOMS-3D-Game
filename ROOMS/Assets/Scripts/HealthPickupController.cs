using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupController : MonoBehaviour
{
    [SerializeField] private int healAmount = 10;
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStatus playerStatus = other.GetComponent<PlayerStatus>();
            if (playerStatus != null)
            {
                playerStatus.Heal(healAmount);
                Destroy(gameObject);
            }

            AudioManager audioManager = FindObjectOfType<AudioManager>();
            if (audioManager != null && pickupSound != null)
            {
                audioManager.PlaySFX(pickupSound);
            }
        }
    }
}

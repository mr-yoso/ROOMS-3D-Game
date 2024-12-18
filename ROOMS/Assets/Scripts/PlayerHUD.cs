using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI currentHealthText;
    public TextMeshProUGUI maxHealthText;
    public TextMeshProUGUI speedBoostText;

    private void Start()
    {
        if (speedBoostText != null)
        {
            speedBoostText.enabled = false;
        }
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        currentHealthText.text = currentHealth.ToString();
        maxHealthText.text = maxHealth.ToString();
    }

    public void ShowSpeedBoost(bool isActive)
    {
        if (speedBoostText != null)
        {
            speedBoostText.enabled = isActive; 
            speedBoostText.text = isActive ? "Speed Boost Active!" : "";
        }
    }
}

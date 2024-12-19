using UnityEngine;
using UnityEngine.UI;

public class BossHUD : MonoBehaviour
{
    public Slider bossHealthBar; // Reference to the boss health bar slider

    public void ShowBossHealthBar(float maxHealth)
    {
        if (bossHealthBar != null)
        {
            bossHealthBar.gameObject.SetActive(true); // Show the health bar
            bossHealthBar.maxValue = maxHealth;
            bossHealthBar.value = maxHealth;
        }
    }

    public void UpdateBossHealth(float currentHealth)
    {
        if (bossHealthBar != null)
        {
            bossHealthBar.value = currentHealth; // Update the health value
        }
    }

    public void HideBossHealthBar()
    {
        if (bossHealthBar != null)
        {
            bossHealthBar.gameObject.SetActive(false); // Hide the health bar
        }
    }
}

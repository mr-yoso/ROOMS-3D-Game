using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI currentHealthText;
    public TextMeshProUGUI maxHealthText;

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        currentHealthText.text = currentHealth.ToString();
        maxHealthText.text = maxHealth.ToString();
    }
}

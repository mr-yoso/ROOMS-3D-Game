using UnityEngine;
using TMPro;

public class PowerHUD : MonoBehaviour
{
    public TextMeshProUGUI damagePowerText; // Text for Damage Power
    public TextMeshProUGUI speedPowerText;  // Text for Speed Power

    private void Start()
    {
        // Ensure both texts are initially empty or hidden
        if (damagePowerText != null)
        {
            damagePowerText.text = "";
        }
        if (speedPowerText != null)
        {
            speedPowerText.text = "";
        }
    }

    public void ShowPowerStatus(string powerType, bool isActive)
    {
        if (powerType == "Damage" && damagePowerText != null)
        {
            damagePowerText.text = isActive ? "Damage Boost Active!" : "";
        }
        else if (powerType == "Speed" && speedPowerText != null)
        {
            speedPowerText.text = isActive ? "Speed Boost Active!" : "";
        }
    }
}

using UnityEngine;

public class BossController : AIController
{
    [SerializeField] private float speedMultiplier = 1.5f;
    [SerializeField] private float healthMultiplier = 10f;
    [SerializeField] private float sightRangeMultiplier = 2f;
    private BossHUD bossHUD;

    private void Start()
    {
        // Increase speed and health for the boss
        agent.speed *= speedMultiplier;
        health *= healthMultiplier;

        // Increase the sight range for the boss
        sightRange *= sightRangeMultiplier;

        // Find the BossHUD and show the boss health bar
        bossHUD = FindObjectOfType<BossHUD>();
        if (bossHUD != null)
        {
            bossHUD.ShowBossHealthBar(health);
        }

        Debug.Log($"Boss initialized with {health} health, {agent.speed} speed, and {sightRange} sight range.");
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        // Update the boss health bar
        if (bossHUD != null)
        {
            bossHUD.UpdateBossHealth(health);
        }

        // Hide the boss health bar when the boss dies
        if (health <= 0 && bossHUD != null)
        {
            bossHUD.HideBossHealthBar();
        }
    }
}

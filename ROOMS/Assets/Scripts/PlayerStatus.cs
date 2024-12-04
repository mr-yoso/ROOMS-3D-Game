using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : CharacterStatus
{
    private PlayerHUD hud;

    void Start()
    {
        hud = GetComponent<PlayerHUD>();
        InitVariables();
    }

    public override void CheckHealth()
    {
        base.CheckHealth();
        hud.UpdateHealth(health, maxHealth);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthPowerUp : PowerUp
{
    private float healthIncrease = 25f;

    public override string GetStatsInfo()
    {
        return $"Max HP  + {healthIncrease}";
    }

    public override void ApplyPowerUp(PlayerController player)
    {
        player.IncreaseMaxHealth(healthIncrease);
    }
}

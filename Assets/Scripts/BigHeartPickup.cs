using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigHeartPickup : Pickup
{
    private float healAmount = 50f;

    protected override void ApplyEffectTo(PlayerController player)
    {
        player.Heal(healAmount);
    }
}

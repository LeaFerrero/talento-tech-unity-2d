using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class SmallHeartPickup : Pickup
{
    private float healAmount = 25f;
    
    protected override void ApplyEffectTo(PlayerController player)
    {
        player.Heal(healAmount);
    }
}

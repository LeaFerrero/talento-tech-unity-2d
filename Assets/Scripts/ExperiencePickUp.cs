using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePickUp : Pickup
{
    [SerializeField] private float experienceAmount;

    protected override void ApplyEffectTo(PlayerController player)
    {
        player.ExperienceUp(experienceAmount);
    }
}

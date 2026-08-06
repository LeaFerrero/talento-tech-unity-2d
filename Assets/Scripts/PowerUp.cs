using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PowerUp", menuName = "PowerUp")]
public class PowerUp : ScriptableObject
{
    [SerializeField] private string powerUpName;
    [SerializeField] private string powerUpDescription;
    [SerializeField] private Sprite powerUpIcon;

    public string Name => powerUpName;
    public string Description => powerUpDescription;
    public Sprite Icon => powerUpIcon;

    public virtual string GetStatsInfo() 
    {
        return $"{Name}: {Description}";
    }

    public virtual void ApplyPowerUp(PlayerController player)
    {
        Debug.Log("Aplicando power-up: " + Name);
    }

    public virtual void ApplyPowerUp(PlayerController player, WeaponDirection weaponDirection)
    {
        Debug.Log("Aplicando power-up: " + Name);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

[CreateAssetMenu(fileName = "New WeaponPowerUp", menuName = "PowerUp/Weapon")]
public class WeaponPowerUp : PowerUp
{
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private WeaponData weaponData;

    public override string GetStatsInfo()
    {
        string info = $"Damage: {weaponData.Damage}\n" +
                      $"Fire rate: {weaponData.FireRate}\n";

        switch (weaponData.WeaponType)
        {
            case WeaponType.Ricochet:
                info += $"Max Bounces: {weaponData.MaxBounces}\n";
                break;
            case WeaponType.Piercing:
                info += $"Pierce: {weaponData.MaxPierces}";
                break;
        }       

        return info;
    }

    public override void ApplyPowerUp(PlayerController player, WeaponDirection weaponDirection)
    {
        player.EquipWeapon(weaponPrefab, weaponDirection);
    }
}

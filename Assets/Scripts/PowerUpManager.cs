using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private List<PowerUp> allPowerUps;
    [SerializeField] private PowerUpMenu powerUpMenu;
    [SerializeField] private WeaponDirectionMenu weaponDirectionMenu;
    [SerializeField] private PlayerWeaponsUI playerWeaposUI;
    private List<PowerUp> selectedPowerUps;
    private List<PowerUp> availablePowerUps;

    private void Start()
    {
        powerUpMenu.ClosePowerUpMenu();
    }

    public void ShowPowerUps()
    {
        selectedPowerUps = new List<PowerUp>();
        availablePowerUps = new List<PowerUp>(allPowerUps);

        for (int i = 0; i < 2 && availablePowerUps.Count > 0; i++)
        {
            int index = Random.Range(0, availablePowerUps.Count);
            selectedPowerUps.Add(availablePowerUps[index]);
            availablePowerUps.RemoveAt(index);
        }
        
        powerUpMenu.ShowPowerUpMenu(selectedPowerUps);
    }

    public void ChoosePowerUp(PowerUp powerUp, PlayerController player)
    {
        powerUpMenu.ClosePowerUpMenu();

        if (powerUp is WeaponPowerUp weaponPowerUp)
        {
            weaponDirectionMenu.ShowWeaponDirectionMenu(direction =>
            {
                weaponPowerUp.ApplyPowerUp(player, direction);
                playerWeaposUI.SetWeaponIcon(weaponPowerUp.Icon, direction);
            });
        }
        else
        {
            powerUp.ApplyPowerUp(player);
        }
    }
}

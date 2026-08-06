using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerUpMenu : MonoBehaviour
{
    [SerializeField]
    private List<PowerUpSlot> powerUpSlots;
    //[SerializeField] private List<Button> buttons;
    [SerializeField] private PowerUpManager manager;
    [SerializeField] private PlayerController player;
    
    public void ShowPowerUpMenu(List<PowerUp> powerUpOptions)
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        for (int i = 0; i < powerUpSlots.Count; i++)
        {
            int index = i; // Importante para el closure
            if (i < powerUpOptions.Count)
            {
                powerUpSlots[i].button.gameObject.SetActive(true);
                powerUpSlots[i].powerUpNameText.text = powerUpOptions[i].Name;
                powerUpSlots[i].powerUpImage.sprite = powerUpOptions[i].Icon;
                powerUpSlots[i].powerUpDescriptionText.text = powerUpOptions[i].GetStatsInfo();

                // Configurás el botón para que al hacer click elija ese powerup
                PowerUp powerUp = powerUpOptions[i]; // importante para el closure
                powerUpSlots[i].button.onClick.RemoveAllListeners();
                powerUpSlots[i].button.onClick.AddListener(() => manager.ChoosePowerUp(powerUp, player));
            }
            else
            {
                powerUpSlots[i].button.gameObject.SetActive(false);
            }
        }
    }

    public void ClosePowerUpMenu()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}

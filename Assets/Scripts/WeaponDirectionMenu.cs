using System;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDirectionMenu : MonoBehaviour
{
    [SerializeField] private Button frontButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button backButton;

    private Action<WeaponDirection> onDirectionChosen;



    public void ShowWeaponDirectionMenu(Action<WeaponDirection> callback)
    {
        onDirectionChosen = callback;

        gameObject.SetActive(true);
        Time.timeScale = 0f;

        frontButton.onClick.RemoveAllListeners();
        frontButton.onClick.AddListener(() => SelectDirection(WeaponDirection.Front));

        leftButton.onClick.RemoveAllListeners();
        leftButton.onClick.AddListener(() => SelectDirection(WeaponDirection.Left));

        rightButton.onClick.RemoveAllListeners();
        rightButton.onClick.AddListener(() => SelectDirection(WeaponDirection.Right));

        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(() => SelectDirection(WeaponDirection.Back));
    }

    private void SelectDirection(WeaponDirection direction)
    {
        onDirectionChosen?.Invoke(direction);
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}

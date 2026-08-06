using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponsUI : MonoBehaviour
{
    [SerializeField] private Image frontWeaponIcon;
    [SerializeField] private Image leftWeaponIcon;
    [SerializeField] private Image rightWeaponIcon;
    [SerializeField] private Image backWeaponIcon;

    public void SetWeaponIcon(Sprite icon, WeaponDirection direction)
    {
        switch (direction)
        {
            case WeaponDirection.Front:
                frontWeaponIcon.sprite = icon;
                break;
            case WeaponDirection.Left:
                leftWeaponIcon.sprite = icon;
                break;
            case WeaponDirection.Right:
                rightWeaponIcon.sprite = icon;
                break;
            case WeaponDirection.Back:
                backWeaponIcon.sprite = icon;
                break;
        }
    }
}

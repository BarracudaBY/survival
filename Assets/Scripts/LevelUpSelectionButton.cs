using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSelectionButton : MonoBehaviour
{
    public TMP_Text UpgradeDescriptionText, NameLevelText;
    public Image WeaponIcon;

    private Weapon _asignetWeapon;

    public void UpdateButtonDispay(Weapon theWeapon)
    {
        if(theWeapon.gameObject.activeSelf == true)
        {
            UpgradeDescriptionText.text = theWeapon.Stats[theWeapon.WeaponLvl].Upgratetext;
            WeaponIcon.sprite = theWeapon.Icon;
            NameLevelText.text = theWeapon.name + " - Lvl " + theWeapon.WeaponLvl;
        }
        else
        {
            UpgradeDescriptionText.text = " Unlock " + theWeapon.name;
            WeaponIcon.sprite = theWeapon.Icon;
            NameLevelText.text = theWeapon.name;
        }
        

        _asignetWeapon = theWeapon;
    }

    public void SelectUpgrade()
    {
        if(_asignetWeapon != null)
        {
            if(_asignetWeapon.gameObject.activeSelf == true)
            {
                _asignetWeapon.LevelUp();

            }
            else
            {
                PlayerController.Instance.AddWeapon(_asignetWeapon);
            }
            
            UIController.Instance.LevelUpPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}

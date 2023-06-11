using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpLvlController : MonoBehaviour
{
    public static ExpLvlController Instance;

    public int CurrentExp;
    public ExpPickUp PickUp;

    public List<int> ExpLevels;
    public int CurrentLevel = 1, LevelCount = 100;
    public List<Weapon> WeaponToUpgrade;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        while (ExpLevels.Count < LevelCount)
        {
            ExpLevels.Add(Mathf.CeilToInt(ExpLevels[ExpLevels.Count - 1] * 1.1f));
        }
    }

    public void GetExp(int amountToGet)
    {
        CurrentExp += amountToGet;

        if(CurrentExp >= ExpLevels[CurrentLevel])
        {
            LevelUp();
        }

        UIController.Instance.UpdateExp(CurrentExp, ExpLevels[CurrentLevel], CurrentLevel);

        SFXManager.Instance.PlaySfxPitched(2);
    }

    public void SpawnExp(Vector3 position, int expValue)
    {
        Instantiate(PickUp, position, Quaternion.identity).ExpValue = expValue;
    }

    void LevelUp()
    {
        CurrentExp -= ExpLevels[CurrentLevel];
        CurrentLevel++;

        if (CurrentLevel >= ExpLevels.Count)
        {
            CurrentLevel = ExpLevels.Count - 1;
        }

        //PlayerController.Instance.ActiveWeapon.LevelUp();

        UIController.Instance.LevelUpPanel.SetActive(true);

        Time.timeScale = 0f;

        //UIController.Instance.LevelUpButtons[0].UpdateButtonDispay(PlayerController.Instance.ActiveWeapon);
        //UIController.Instance.LevelUpButtons[0].UpdateButtonDispay(PlayerController.Instance.AssignetWeapon[0]);

        //UIController.Instance.LevelUpButtons[1].UpdateButtonDispay(PlayerController.Instance.UnassignetWeapon[0]);
        //UIController.Instance.LevelUpButtons[2].UpdateButtonDispay(PlayerController.Instance.UnassignetWeapon[1]);

        WeaponToUpgrade.Clear();

        List<Weapon> avalableWeapons = new List<Weapon>();
        avalableWeapons.AddRange(PlayerController.Instance.AssignetWeapon);

        if (avalableWeapons.Count > 0)
        {
            int selected = Random.Range(0, avalableWeapons.Count);
            WeaponToUpgrade.Add(avalableWeapons[selected]);
            avalableWeapons.RemoveAt(selected);
        }

        if (PlayerController.Instance.AssignetWeapon.Count + PlayerController.Instance.FullyLeveledWeapons.Count < PlayerController.Instance.MaxWeapons)
        {
            avalableWeapons.AddRange(PlayerController.Instance.UnassignetWeapon);
        }

        for (int i = WeaponToUpgrade.Count; i < 3; i++)
        {
            if (avalableWeapons.Count > 0)
            {
                int selected = Random.Range(0, avalableWeapons.Count);
                WeaponToUpgrade.Add(avalableWeapons[selected]);
                avalableWeapons.RemoveAt(selected);
            }
        }

        for (int i = 0; i < WeaponToUpgrade.Count; i++)
        {
            UIController.Instance.LevelUpButtons[i].UpdateButtonDispay(WeaponToUpgrade[i]);
        }

        for (int i = 0; i < UIController.Instance.LevelUpButtons.Length; i++)
        {
            if(i < WeaponToUpgrade.Count)
            {
                UIController.Instance.LevelUpButtons[i].gameObject.SetActive(true);
            }
            else
            {
                UIController.Instance.LevelUpButtons[i].gameObject.SetActive(false);
            }
        }

        PlayerStatsController.Instance.UpdateDisplay();
    }
}

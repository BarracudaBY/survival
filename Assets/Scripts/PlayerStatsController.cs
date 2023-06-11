using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    public static PlayerStatsController Instance;

    public List<PlayerStatValue> MoveSpeed, Healt, pickUpRange, MaxWeapons;
    public int MoveSpeedLevelCount, HealtLevelCount, PickUpRangeLevelCount;

    public int MoveSpeedLevel, HealthLevel, PickUpLevel, MaxWeaponsLevel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = MoveSpeed.Count -1; i < MoveSpeedLevelCount; i++)
        {
            MoveSpeed.Add(new PlayerStatValue(MoveSpeed[i].Cost + MoveSpeed[1].Cost, MoveSpeed[i].Value + (MoveSpeed[1].Value - MoveSpeed[0].Value)));
        }

        for (int i = Healt.Count - 1; i < HealtLevelCount; i++)
        {
            Healt.Add(new PlayerStatValue(Healt[i].Cost + Healt[1].Cost, Healt[i].Value + (Healt[1].Value - Healt[0].Value)));
        }

        for (int i = pickUpRange.Count - 1; i < PickUpRangeLevelCount; i++)
        {
            pickUpRange.Add(new PlayerStatValue(pickUpRange[i].Cost + pickUpRange[1].Cost, pickUpRange[i].Value + (pickUpRange[1].Value - pickUpRange[0].Value)));
        }
    }

    private void Update()
    {
        if(UIController.Instance.LevelUpPanel.activeSelf == true)
        {
            UpdateDisplay();
        }
    }

    public void UpdateDisplay()
    {
        if(MoveSpeedLevel < MoveSpeed.Count - 1)
        {
            UIController.Instance.MoveSpeedUpgradeDisplay.UpdateDisplay(MoveSpeed[MoveSpeedLevel + 1].Cost, MoveSpeed[MoveSpeedLevel].Value, MoveSpeed[MoveSpeedLevel + 1].Value);

        }
        else
        {
            UIController.Instance.MoveSpeedUpgradeDisplay.ShowMaxLevel();
        }

        if (HealthLevel < Healt.Count - 1)
            UIController.Instance.HealthUpgradeDisplay.UpdateDisplay(Healt[HealthLevel + 1].Cost, Healt[HealthLevel].Value, Healt[HealthLevel + 1].Value);
        else
            UIController.Instance.HealthUpgradeDisplay.ShowMaxLevel();

        if (PickUpLevel < pickUpRange.Count - 1)
            UIController.Instance.PickUpRangeUpdateDisplay.UpdateDisplay(pickUpRange[PickUpLevel + 1].Cost, pickUpRange[PickUpLevel].Value, pickUpRange[PickUpLevel + 1].Value);
        else
            UIController.Instance.PickUpRangeUpdateDisplay.ShowMaxLevel();

        if (MaxWeaponsLevel < MaxWeapons.Count - 1)
            UIController.Instance.MaxWeaponsUpdateDisplay.UpdateDisplay(MaxWeapons[MaxWeaponsLevel + 1].Cost, MaxWeapons[MaxWeaponsLevel].Value, MaxWeapons[MaxWeaponsLevel + 1].Value);
        else
            UIController.Instance.MaxWeaponsUpdateDisplay.ShowMaxLevel();
    }

    public void PurchaseMoveSpeed()
    {
        MoveSpeedLevel++;

        CoinController.Instance.SpendCoin(MoveSpeed[MoveSpeedLevel].Cost);

        UpdateDisplay();

        PlayerController.Instance.MoveSpeed = MoveSpeed[MoveSpeedLevel].Value;
    }

    public void PurchaseHealth()
    {
        HealthLevel++;

        CoinController.Instance.SpendCoin(Healt[HealthLevel].Cost);

        UpdateDisplay();

        PlayerHealthController.Instance.MaxHealth = Healt[HealthLevel].Value;
        PlayerHealthController.Instance.CurrentHealth += Healt[HealthLevel].Value - Healt[HealthLevel - 1].Value;
    }

    public void PurchasePickUpRange()
    {
        PickUpLevel++;

        CoinController.Instance.SpendCoin(pickUpRange[PickUpLevel].Cost);

        UpdateDisplay();

        PlayerController.Instance.PickUpRange = pickUpRange[PickUpLevel].Value;
    }

    public void PurchaseMaxWeapons()
    {
        MaxWeaponsLevel++;

        CoinController.Instance.SpendCoin(MaxWeapons[MaxWeaponsLevel].Cost);

        UpdateDisplay();

        PlayerController.Instance.MaxWeapons = Mathf.RoundToInt(MaxWeapons[MaxWeaponsLevel].Value);
    }

    public void GoToDefaultMoveSpeed()
    {
        PlayerController.Instance.MoveSpeed = MoveSpeed[MoveSpeedLevel].Value;
    }
}

[System.Serializable]
public class PlayerStatValue
{
    public int Cost;
    public float Value;

    public PlayerStatValue (int newCost, float newValue)
    {
        Cost = newCost;
        Value = newValue;
    }
}

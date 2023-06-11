using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatUpgradeDisplay : MonoBehaviour
{
    public TMP_Text CostText, ValueText;

    public GameObject UpgradeButton;

    public void UpdateDisplay(int cost, float oldValue, float newValue)
    {
        ValueText.text = "Value: " + oldValue.ToString("F1") + "->" + newValue.ToString("F1");
        CostText.text = "Cost: " + cost;

        if(cost <= CoinController.Instance.CurrentCoin)
        {
            UpgradeButton.SetActive(true);
        }
        else
        {
            UpgradeButton.SetActive(false);
        }
    }

    public void ShowMaxLevel()
    {
        ValueText.text = "Max Level";
        CostText.text = "Max Level";
        UpgradeButton.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public static CoinController Instance;
    public int CurrentCoin;

    public CoinPickUp Coin;

    private void Awake()
    {
        Instance = this;
    }

    public void AddCoin(int coinsToAdd)
    {
        CurrentCoin += coinsToAdd;

        UIController.Instance.UpdateCoins();

        SFXManager.Instance.PlaySfxPitched(2);
    }

    public void DropCoin(Vector3 position , int value)
    {
        CoinPickUp newCoin = Instantiate(Coin, position + new Vector3(.2f, .1f, 0f), Quaternion.identity);
        newCoin.CoinAmout = value;
        newCoin.gameObject.SetActive(true);
    }

    public void SpendCoin(int coinToSpend)
    {
        CurrentCoin -= coinToSpend;

        UIController.Instance.UpdateCoins();
    }
}

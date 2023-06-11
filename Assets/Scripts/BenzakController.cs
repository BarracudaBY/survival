using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenzakController : MonoBehaviour
{
    public static BenzakController Instance;

    public float CurrentBenzak;

    public BenzakPickUp Benzak;

    private void Awake()
    {
        Instance = this;
    }

    public void AddBenzak(int benzakToAdd)
    {
        CurrentBenzak += benzakToAdd;

        UIController.Instance.UpdateBenzak();
    }

    public void DropBenzak(Vector3 position, int value)
    {
        BenzakPickUp newBenzak = Instantiate(Benzak, position + new Vector3(.3f, .2f, 0f), Quaternion.identity);
        Benzak.BenzakAmout = value;
        Benzak.gameObject.SetActive(true);
    }

    public void SpendBenzak(int benzakToSpend)
    {
        CurrentBenzak -= benzakToSpend;

        UIController.Instance.UpdateBenzak();
    }
}

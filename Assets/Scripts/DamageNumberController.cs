using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    public static DamageNumberController Instance;

    public DamageNumber NumberToSpawn;
    public Transform NumberCanvas;

    private List<DamageNumber> _numberPool = new List<DamageNumber>();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.U))
        {
            SpawnDamage(57f, new Vector3(3, 5, 0));
        }*/
    }

    public void SpawnDamage(float damageAmout, Vector3 location)
    {
        int raundet = Mathf.RoundToInt(damageAmout);

        //DamageNumber newDamage =  Instantiate(NumberToSpawn, location, Quaternion.identity , NumberCanvas);

        DamageNumber newDamage = GetFromPool();

        newDamage.SetUp(raundet);
        newDamage.gameObject.SetActive(true);
        newDamage.transform.position = location;
    }

    public DamageNumber GetFromPool()
    {
        DamageNumber numberToOut = null;

        if(_numberPool.Count == 0)
        {
            numberToOut = Instantiate(NumberToSpawn, NumberCanvas);
        }
        else
        {
            numberToOut = _numberPool[0];
            _numberPool.RemoveAt(0);
        }


        return numberToOut;
    }

    public void PlaceInPool(DamageNumber numberToPlace)
    {
        numberToPlace.gameObject.SetActive(false);
        _numberPool.Add(numberToPlace);
    }
}

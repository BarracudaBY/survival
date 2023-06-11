using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public TMP_Text DamageText;
    public float LifeTime;
    public float FloatSpeed = 1f;

    private float _lifeCounter;

    private void Update()
    {
        if(_lifeCounter > 0)
        {
            _lifeCounter -= Time.deltaTime;

            if(_lifeCounter <= 0)
            {
                //Destroy(gameObject);

                DamageNumberController.Instance.PlaceInPool(this);
            }
        }

        /*if (Input.GetKeyDown(KeyCode.K))
        {
            SetUp(45);
        }*/

        transform.position += Vector3.up * FloatSpeed * Time.deltaTime;
    }

    public void SetUp(int damageDisplay)
    {
        _lifeCounter = LifeTime;

        DamageText.text = damageDisplay.ToString();
    }
}

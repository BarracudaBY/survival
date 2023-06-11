using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldTurel : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;

    [SerializeField] private Slider _healthSlider;

    private void Start()
    {
        CurrentHealth = MaxHealth;
        _healthSlider.maxValue = MaxHealth;
        _healthSlider.value = CurrentHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if(CurrentHealth <= 0)
        {
            Destroy(this);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController Instance;

    public float CurrentHealth, MaxHealth;
    public Slider HealthSlider;

    public GameObject DeathFX;

    public Image RedOverlay;

    Color AlphaColor;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        MaxHealth = PlayerStatsController.Instance.Healt[0].Value;
        CurrentHealth = MaxHealth;
        HealthSlider.maxValue = MaxHealth;
        HealthSlider.value = CurrentHealth;

        AlphaColor = RedOverlay.color;

    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        //RedOverlay.SetActive(true);
        AlphaColor.a += 0.1f;
        RedOverlay.color = AlphaColor;

        SFXManager.Instance.PlaySfx(11);

        if (CurrentHealth <= 0)
        {
            gameObject.SetActive(false);

            LevelManager.Instance.EndLevel();

            Instantiate(DeathFX, transform.position, transform.rotation);

            SFXManager.Instance.PlaySfx(3);
        }

        HealthSlider.value = CurrentHealth;

        //yield return new WaitForSeconds(0.5f);

        //RedOverlay.SetActive(false);
    }
}

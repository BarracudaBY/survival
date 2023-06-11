using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Slider ExpLvlSlider;
    public TMP_Text ExpLvlText;

    public LevelUpSelectionButton[] LevelUpButtons;

    public GameObject LevelUpPanel;
    public GameObject LevelEndScreen;
    public GameObject PausedScreen;

    public TMP_Text CoinText;
    public TMP_Text BenzakText;
    public TMP_Text TimeText;
    public TMP_Text EndTimeText;

    public PlayerStatUpgradeDisplay MoveSpeedUpgradeDisplay, HealthUpgradeDisplay, PickUpRangeUpdateDisplay, MaxWeaponsUpdateDisplay;

    public string MainMenuName;


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseAndUnpause();
        }
    }

    public void UpdateExp(int currentExp, int levelExp, int currentLvl)
    {
        ExpLvlSlider.maxValue = levelExp;
        ExpLvlSlider.value = currentExp;

        ExpLvlText.text = "Level: " + currentLvl;
    }

    public void SkipLevelUp()
    {
        LevelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void UpdateCoins()
    {
        CoinText.text = "Coin: " + CoinController.Instance.CurrentCoin;
    }

    public void UpdateBenzak()
    {
        BenzakText.text = "Benzak: " + BenzakController.Instance.CurrentBenzak;
    }

    public void PurchaseMoveSpeed()
    {
        PlayerStatsController.Instance.PurchaseMoveSpeed();
        SkipLevelUp();
    }

    public void PurchaseHealth()
    {
        PlayerStatsController.Instance.PurchaseHealth();
        SkipLevelUp();
    }

    public void PurchasePickUpRange()
    {
        PlayerStatsController.Instance.PurchasePickUpRange();
        SkipLevelUp();
    }

    public void PurchaseMaxWeapons()
    {
        PlayerStatsController.Instance.PurchaseMaxWeapons();
        SkipLevelUp();
    }

    public void UpdateTimer(float time)
    {
        float minutes = Mathf.FloorToInt( time / 60f);
        float seconds = Mathf.FloorToInt( time % 60);

        TimeText.text = "Time: " + minutes + ":" + seconds.ToString("00");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(MainMenuName);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseAndUnpause()
    {
        if(PausedScreen.activeSelf == false)
        {
            PausedScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            PausedScreen.SetActive(false);
            if(LevelUpPanel.activeSelf == false) 
                Time.timeScale = 1f;
        }
    }

}

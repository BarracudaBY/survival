using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private bool _gameActive;
    public float Timer;

    public float WaitToShowEndScreen = 1f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _gameActive = true;
    }

    private void Update()
    {
        if(_gameActive == true)
        {
            Timer += Time.deltaTime;

            UIController.Instance.UpdateTimer(Timer);
        }
        
    }

    public void EndLevel()
    {
        _gameActive = false;

        StartCoroutine(EndLevelCo());
    }

    IEnumerator EndLevelCo()
    {
        yield return new WaitForSeconds(WaitToShowEndScreen);

        float minutes = Mathf.FloorToInt(Timer / 60f);
        float seconds = Mathf.FloorToInt(Timer % 60);

        UIController.Instance.EndTimeText.text = minutes.ToString() + "MINUTS" + seconds.ToString("00") + "SECS";
        UIController.Instance.LevelEndScreen.SetActive(true);
    }


}

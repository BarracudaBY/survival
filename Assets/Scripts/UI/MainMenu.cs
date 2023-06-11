using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string FirstLevelGame;

    public void StartGame()
    {
        SceneManager.LoadScene(FirstLevelGame);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log(" I'm Quiting");
    }
}

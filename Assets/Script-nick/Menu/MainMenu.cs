using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// When this function is triggered it starts teh game by transporting you to the game scene
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// When this function is triggered it show the tutorial window while closing all other windows
    /// </summary>
    public void ViewTutorial()
    {

    }

    /// <summary>
    /// When this function is triggered it shows the settings window while closing other windows.
    /// </summary>
    public void ViewSettings()
    {

    }

    /// <summary>
    /// When this function is triggered it closes the game
    /// </summary>
    public void quitGame()
    {
        Application.Quit();
    }
}

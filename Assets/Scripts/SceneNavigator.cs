using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    public static void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public static void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void GoToLevel1()
    {
        SceneManager.LoadScene("GameScreen");
    }

    public static void GoToGameOver()
    {
        SceneManager.LoadScene("GameOverScreen");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("GameScreen");
    }

    public void GoToGameOver()
    {
        SceneManager.LoadScene("GameOverScreen");
    }
}

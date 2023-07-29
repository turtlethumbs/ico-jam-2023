using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int money = 0;
    public float difficulty = 1f;
    public float difficultyMultiplier = 0.5f;
    public float difficultyIncreaseInterval = 30f;
    public float level = 0f;
    public float timer = 0f;
    public HourGlass theHourGlass;

    void Start()
    {
        timer = 0f;
        InvokeRepeating("IncreaseDifficulty", difficultyIncreaseInterval, difficultyIncreaseInterval);
        level = difficulty;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (theHourGlass.isSelfDestructing)
        {
            GameOver();
        }
    }

    void IncreaseDifficulty()
    {
        difficulty *= 1 + difficultyMultiplier;
        level = difficulty;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("GameScreen");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScreen");
    }
}

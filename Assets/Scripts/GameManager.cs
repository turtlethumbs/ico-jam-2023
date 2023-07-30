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
    private AudioSource audioSource;
    private bool hasBellRing1Sounded = false;
    private bool hasBellRing2Sounded = false;
    private bool hasBellRing3Sounded = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timer = 0f;
        InvokeRepeating("IncreaseDifficulty", difficultyIncreaseInterval, difficultyIncreaseInterval);
        level = difficulty;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!hasBellRing1Sounded && timer >= 30f)
        {
            PlayNextBellRingSound();
            hasBellRing1Sounded = true;
        }
        if (!hasBellRing2Sounded && timer >= 60f)
        {
            PlayNextBellRingSound();
            hasBellRing2Sounded = true;
        }
        if(!hasBellRing3Sounded && timer >= 90f)
        {
            PlayNextBellRingSound();
            hasBellRing3Sounded = true;
        }

        if (theHourGlass.isSelfDestructing)
        {
            GameOver();
        }
    }

    void PlayNextBellRingSound()
    {
        audioSource.Play();
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

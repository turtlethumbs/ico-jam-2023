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
    public Player thePlayer;
    public HourGlass theHourGlass;
    private AudioSource audioSource;
    private AudioDistortionFilter audioDistortionFilter;
    private bool hasBellRing1Sounded = false;
    private bool hasBellRing2Sounded = false;
    private bool hasBellRing3Sounded = false;
    private bool hasPlayerWon = false;
    private float gameOverTimeOut = 3f;
    
    private VignetteController vignetteController;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioDistortionFilter = GetComponent<AudioDistortionFilter>();
        vignetteController = GetComponent<VignetteController>();
        timer = 0f;
        level = difficulty;
        InvokeRepeating("IncreaseDifficulty", difficultyIncreaseInterval, difficultyIncreaseInterval);
    }

    void Update()
    {
        if (hasPlayerWon) return;
        timer += Time.deltaTime;
        if (!vignetteController.enabled && theHourGlass.isSelfDestructing)
        {
            thePlayer.enabled = false;
            vignetteController.enabled = true;
            Invoke("GameOver", gameOverTimeOut);
        }
        if (!hasBellRing1Sounded && timer >= 30f)
        {
            audioDistortionFilter.distortionLevel = 0.5f;
            PlayNextBellRingSound();
            hasBellRing1Sounded = true;
        }
        if (!hasBellRing2Sounded && timer >= 60f)
        {
            audioDistortionFilter.distortionLevel = 0.7f;
            PlayNextBellRingSound();
            hasBellRing2Sounded = true;
        }
        if(!hasBellRing3Sounded && timer >= 90f)
        {
            audioDistortionFilter.distortionLevel = 0.9f;
            PlayNextBellRingSound();
            vignetteController.enabled = true;
            thePlayer.gameObject.SetActive(false);
            theHourGlass.enabled = false;
            hasBellRing3Sounded = true;
            hasPlayerWon = true;
            Invoke("PlayerWon", gameOverTimeOut);
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

    public void PlayerWon()
    {
        SceneManager.LoadScene("Credits");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScreen");
    }
}

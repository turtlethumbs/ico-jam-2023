using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int money = 0;
    public float difficulty = 1f;
    public float difficultyMultiplier = 0.5f;
    public float difficultyIncreaseInterval = 30f;
    public float level = 0f;
    public float timer = 0f;

    void Start()
    {
        timer = 0f;
        InvokeRepeating("IncreaseDifficulty", difficultyIncreaseInterval, difficultyIncreaseInterval);
        level = difficulty;
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    void IncreaseDifficulty()
    {
        difficulty *= 1 + difficultyMultiplier;
        level = difficulty;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float itemSpawnCounter;
    private float enemySpawnCounter;
    private float itemSpawnTime;
    private float enemySpawnTime;
    private float enemySpawnDecrementer;
    private int difficulty;

    public ScoreManager scoreManager;
    public ItemSpawn itemSpawn;
    public float warningDelay;
    public bool gameIsOver = false;

    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();

        //Deactivate control overlay on PC version
        #if UNITY_ANDROID || UNITY_IOS
        #else
            GameObject controlOverlay = GameObject.FindGameObjectWithTag("ControlOverlay");
            if (controlOverlay)
            {
                controlOverlay.SetActive(false);
            }
        #endif
    }

    private void Start()
    {
        SetDifficulty();
    }

    void Update ()
    {
        if (!gameIsOver)
        {
            // Increments spawnCounter timer
            itemSpawnCounter += Time.deltaTime;
            enemySpawnCounter += Time.deltaTime;

            // Spawns a new fruit whenever the timer reaches a certain value
            if (itemSpawnCounter >= itemSpawnTime)
            {
                itemSpawn.SpawnFruit();
                itemSpawnCounter = 0;
            }

            // Spawns a new enemy whenever the timer reaches a certain value
            if (enemySpawnCounter >= enemySpawnTime)
            {
                itemSpawn.SpawnEnemy();
                enemySpawnCounter = 0;

                // Decrement in relation to current spawn time
                if(enemySpawnTime >= 3f)
                {
                    enemySpawnTime -= enemySpawnDecrementer;
                }
                else if (enemySpawnTime >= 2f)
                {
                    enemySpawnTime -= enemySpawnDecrementer / 3 * 2;
                }
                else if (enemySpawnTime >= 1f)
                {
                    enemySpawnTime -= enemySpawnDecrementer / 3;
                }
            }
        }
    }

    // Send scores to player prefs
    private void SendScores()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("PlayedScene", currentScene);
        string highscoreKey = "HS";

        PlayerPrefs.SetInt("Score", scoreManager.currentScore);

        switch (difficulty)
        {
            case 0:
                highscoreKey += "Easy";
                break;

            case 1:
                highscoreKey += "Normal";
                break;

            case 2:
                highscoreKey += "Hard";
                break;

            default:
                break;
        }

        switch (currentScene)
        {
            case 2:
                highscoreKey += "StageOne";
                break;

            case 3:
                highscoreKey += "StageTwo";
                break;

            case 4:
                highscoreKey += "StageThree";
                break;
        }

        // Sets Highscore in case it doesn't exist or current score is higher
        if (!PlayerPrefs.HasKey(highscoreKey) || scoreManager.currentScore > PlayerPrefs.GetInt(highscoreKey))
        {
            PlayerPrefs.SetInt(highscoreKey, scoreManager.currentScore);
        }
    }

    // Handles all actions after Game Over
    public void GameOver()
    {
        SendScores();

        gameIsOver = true;
        itemSpawnCounter = 0;
        enemySpawnCounter = 0;

        // Loads game over Screen
        SceneManager.LoadScene(1);
    }

    // Loads Main Menu
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Set difficulty
    private void SetDifficulty()
    {
        difficulty = PlayerPrefs.GetInt("Difficulty");

        switch (difficulty)
        {
            // Easy
            case 0:
                itemSpawnTime = 3f;
                enemySpawnTime = 5.5f;
                enemySpawnDecrementer = 0.15f;
                warningDelay = 0f;
                break;
            
            // Normal
            case 1:
                itemSpawnTime = 2.5f;
                enemySpawnTime = 5f;
                enemySpawnDecrementer = 0.15f;
                warningDelay = 0.1f;
                break;
            
            // Hard
            case 2:
                itemSpawnTime = 2f;
                enemySpawnTime = 4f;
                enemySpawnDecrementer = 0.20f;
                warningDelay = 0.13f;
                break;

            default:
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScoreHandler : MonoBehaviour
{
    private int difficulty;

    public int lastScene;
    public TextMeshProUGUI score;
    public TextMeshProUGUI highscore;
    public TextMeshProUGUI difficultyObj;

	private void Start ()
    {
        SetScore();
        SetDifficultyText();
        SetHighscore();
	}

    // Set Score
    private void SetScore()
    {
        score.text = "Score: " + PlayerPrefs.GetInt("Score").ToString();
    }

    // Set Highscore
    private void SetHighscore()
    {
        int stageHighscore = 0;
        lastScene = PlayerPrefs.GetInt("PlayedScene");

        string highscoreKey = "HS";

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

        switch (lastScene)
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

        stageHighscore = PlayerPrefs.GetInt(highscoreKey);
        highscore.text = "Highscore: " + stageHighscore.ToString();
    }

    // Set text for difficulty
    private void SetDifficultyText()
    {
        difficulty = PlayerPrefs.GetInt("Difficulty");

        switch (difficulty)
        {
            case 0:
                difficultyObj.text = "Easy";
                break;

            case 1:
                difficultyObj.text = "Normal";
                break;

            case 2:
                difficultyObj.text = "Hard";
                break;

            default:
                break;
        }
    }
}

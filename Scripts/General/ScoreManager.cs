using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {

    private TextMeshProUGUI scoreText;

    public int currentScore;
    public int deliveryPoints; // Points per fruit delivery

    private void Awake()
    {
        scoreText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start ()
    {
        currentScore = 0;
        deliveryPoints = 10;

        SetScoreText();
	}
	
    // Increment score upon delivery
    // Made scalable for different occurences by accepting a string input
    public void IncrementScore(string occurence)
    {
        if(occurence == "Item Delivery")
        {
            currentScore += deliveryPoints;
        }

        SetScoreText();
    }

    // Update score text
    public void SetScoreText()
    {
        scoreText.text = "Score: " + currentScore.ToString();
    }
}

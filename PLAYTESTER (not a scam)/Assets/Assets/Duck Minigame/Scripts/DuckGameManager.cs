using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DuckGameManager : MonoBehaviour
{
    public TMP_Text scoreText;  // Reference to TextMeshPro to show the score
    private int score = 0;

    void Start()
    {
        UpdateScoreText();
    }

    // Method to add or subtract points
    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreText();
    }

    // Update the score display
    void UpdateScoreText()
    {
        scoreText.text = "Points: " + score;

    }
}

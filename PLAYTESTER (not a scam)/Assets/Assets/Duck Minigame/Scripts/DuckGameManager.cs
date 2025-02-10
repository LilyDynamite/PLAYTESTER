using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DuckGameManager : MonoBehaviour
{
    public TMP_Text scoreText;  // Reference to TextMeshPro to show the score
    private int score = 0;
    public bool gameOver;
    GameObject UIController;

    float timeRemaining;
    public float gameDuration = 30;

    void Start()
    {
        gameOver = true;
        UpdateScoreText();
        UIController = GameObject.Find("UI Controller");
       
    }

    void Update()
    {
        if (!gameOver)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                GameOver();
            }
        }
    }

    //This function is called by the AppScript and should handle setting up everything for the duck minigame
    public void StartDuckMinigame()
    {
        gameOver = false;
        score = 0;
        timeRemaining = gameDuration;
    }

    public void GameOver()
    {
        gameOver = true;

        //TODO: calculate score
        int scoreChange = -12; //for now just set it to the max

        //Tell the game manager that the game is over
        GameObject.Find("Game Manager").GetComponent<GameManagerScript>().CompletedMinigame(scoreChange);

        //Go to the leaderboard
        UIController.GetComponent<ComputerUIScript>().GoToPosition(new Vector3(90, 20, -10)); //go to the leaderboard
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

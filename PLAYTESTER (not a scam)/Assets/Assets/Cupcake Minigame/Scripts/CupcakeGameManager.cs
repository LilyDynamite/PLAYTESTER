using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CupcakeGameManager : MonoBehaviour
{
    //glitch??
    public float glitchFrequency = 0.5f;
    public float glitchRestTime = 1.5f;

    public float gameDuration = 30f;

    public TMP_Text scoreText;
    public int scorePoints;

    //timer text
    public TMP_Text timerText;
    public float timeRemaining = 0;
    
    private bool gameOver;
    public bool gamePlaying;

    private int timesPlayed;

    public ComputerUIScript UIController;

    //glitches - through frequency will show glitches 
    //have problem i dont know how to add the prefab sprite renderer :(
    // public SpriteRenderer cupcakeRenderer;
    //public Color glitchColor = new Color(255f, 0f, 0f, 1f); // aMgenta
    //private Color normalcupcakeColor;
    //public GameObject prefabCupcake;


    void Start()
    {
        gamePlaying = false;
        timesPlayed = 0;

        UIController = GameObject.Find("UI Controller").GetComponent<ComputerUIScript>();
    }

    //This is the function that will be called by the AppScript script. It should contain
    //everything needed to start the cupcake minigame
    public void StartCupcakeMinigame()
    {

        gameOver = false;
        gamePlaying = true;
        timesPlayed++;

        scorePoints = 0;
        UpdateScoreText();

        //normalcupcakeColor = cupcakeRenderer.color;
        timerText.text = "Time Left: " + gameDuration.ToString();
        //StartCoroutine(GlitchCheckRoutine());


        //set the playCupcakeMinigame in the EnemySpawner script so the game plays
        GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().playCupcakeMinigame = true;
        GameObject.Find("cupcakeHolder").GetComponent<playerMovement>().playCupcakeMinigame = true;

        
        //set the timer for the game
        timeRemaining = gameDuration;

        //If it is the first time playing, tutorial popup
        if(timesPlayed == 1)
        {
            UIController.TriggerPopup(new Vector3(50, 50, -5), "Use the arrow keys to move and space to drop.");
        }

    }

    // Update is called once per frame
    void Update()
    {

        if(gamePlaying)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = "Time Left: " + Mathf.RoundToInt(timeRemaining);

            if(timeRemaining <= 0)
            {
                GameOver();
            }
        }


    }

    private void GameOver()
    {
        gamePlaying = false;
        gameOver = true;
        timerText.text = "Game Stop!";
        //Time.timeScale = 0f; // Stop everything

        //set the enemy spawner script to false
        GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().playCupcakeMinigame = false;

        //TODO: calculate score to send to the hp based on the points earned in the round
        int score = -12; //for now just let this be the max

        //Go back to the main script and say that the game is done:
        GameObject.Find("Game Manager").GetComponent<GameManagerScript>().CompletedMinigame(score);

        GameObject.Find("cupcakeHolder").GetComponent<playerMovement>().playCupcakeMinigame = false;

        //TODO: add a wait a second before go to the leaderboard screen.
        GameObject.Find("UI Controller").GetComponent<ComputerUIScript>().GoToPosition(new Vector3(90, 50, -10)); //go to the leaderboard

    }

    void UpdateScoreText() {
        scoreText.text = "SCORE: " + scorePoints;
    }

    public void AddPoints(int points) { 
        scorePoints+=points;
        UpdateScoreText();
    }





    /*
     aaah
    IEnumerator GlitchCheckRoutine()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(glitchRestTime); // Check every second

            if (glitchFrequency > 0 && Random.value < glitchFrequency) // If we get a random value less than the glitch frequency, the glitches will occur
            {
                StartCoroutine(ActivateGlitch());
            }
        }
    }
    IEnumerator ActivateGlitch()
    {
        // Apply glitch effects
        cupcakeRenderer.color = glitchColor;

        yield return new WaitForSeconds(1f); // Glitch lasts 1 second

        // Revert to normal if the glitch frequency isn't 100%
        if (glitchFrequency != 1)
        {
            cupcakeRenderer.color = normalcupcakeColor;
        }

    }*/
}

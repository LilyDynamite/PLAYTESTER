﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    GameObject UIController;
    GameObject SpawnMg;
    GameObject MainGameManager;

    // Manages glitches 
    public float glitchFreq = 0.5f; // Set based on the day and situation
    public float glitchWaitTime = 1f;
    public float gameDur = 5f;

    // Timer management 
    public TMP_Text timerText;
    private bool isGameOver;

    
    // bg1, bg2 
    // Good spawnable
    // Audio
    // For now, just player color will change
    public SpriteRenderer playerRenderer;
    public Color glitchPlayerColor = new Color(1f, 0f, 1f, 1f); // aMgenta
    private Color normalPlayerColor;

    public int points;

    void Start()
    {
        UIController = GameObject.Find("UI Controller");
        MainGameManager = GameObject.Find("Game Manager");
        SpawnMg = GameObject.Find("SpawnMg");
        normalPlayerColor = playerRenderer.color; // Set starting color as player's uncorrupted color
        isGameOver = true;
        points = 0;

    }

    //This function is called by the AppScript and should start all the setup needed for the minigame
    public void StartCoinMinigame()
    {
        isGameOver = false;
        SpawnMg.GetComponent<spawnMg>().SpawnMgStart();
        StartCoroutine(GlitchCheckRoutine());
        StartCoroutine(StartMinigameTimer());

        points = 0;
        
    }

    IEnumerator GlitchCheckRoutine()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(glitchWaitTime); // Check every second

            if (glitchFreq > 0 && Random.value < glitchFreq) // If we get a random value less than the glitch frequency, the glitches will occur
            {
                StartCoroutine(ActivateGlitch()); 
            }
        }
    }

    IEnumerator ActivateGlitch()
    {
        // Apply glitch effects
        playerRenderer.color = glitchPlayerColor;
      
        yield return new WaitForSeconds(1f); // Glitch lasts 1 second

        // Revert to normal if the glitch frequency isn't 100%
        if (glitchFreq != 1)
        {
           playerRenderer.color = normalPlayerColor;
        }
      
    }

    IEnumerator StartMinigameTimer()
    {
        float timer = gameDur; // Set timer

        while (timer > 0 && !isGameOver)
        {
            timer -= Time.deltaTime; // Update timer
            timerText.text = "Time: " + Mathf.RoundToInt(timer); // Display timer (rounded)
            yield return null;
        }

        GameOver(); // When time is up, call game over function
    }

    void GameOver()
    {
        isGameOver = true;
        timerText.text = "Time's Up!";
        UIController.GetComponent<ComputerUIScript>().GoToPosition(new Vector3(90, 35, -10)); //go to the leaderboard

        //TODO: calculate score change to HP
        int score = -12; //for now just let it be the max

        //tell the game manager that the minigame is done
        MainGameManager.GetComponent<GameManagerScript>().CompletedMinigame(score);

        
        
    }

    //Get the status of the game
    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void AddPoints(int point)
    {
        points += point;
    }
    

    /* This function was supposed to delete all the spawnables after the game ends but it's creating an
     * infinite loop somehow
    
    private void DeleteSpawnables()
    {
        GameObject spawnable = GameObject.Find("Spawnable(Clone)");
        while (spawnable != null)
        {
            Destroy(spawnable);
            spawnable = GameObject.Find("Spawnable(Clone)");
        }

        return;
    }

    */
}


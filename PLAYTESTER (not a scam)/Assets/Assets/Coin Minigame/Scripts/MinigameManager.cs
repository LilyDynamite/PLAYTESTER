using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    GameObject UIController;


    // Manages glitches 
    public float glitchFreq = 0.5f; // Set based on the day and situation
    public float glitchWaitTime = 1f;
    public float gameDur = 30f;

    // Timer management 
    public TMP_Text timerText;
    private bool isGameOver = false;

    
    // bg1, bg2 
    // Good spawnable
    // Audio
    // For now, just player color will change
    public SpriteRenderer playerRenderer;
    public Color glitchPlayerColor = new Color(1f, 0f, 1f, 1f); // aMgenta
    private Color normalPlayerColor;

    void Start()
    {
        UIController = GameObject.Find("UI Controller");

        normalPlayerColor = playerRenderer.color; // Set starting color as player's uncorrupted color

        StartCoroutine(GlitchCheckRoutine()); 
        StartCoroutine(StartMinigameTimer());
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
        Time.timeScale = 0f; // Stop everything
        UIController.GetComponent<ComputerUIScript>().GoToPosition(new Vector3(90, 35, -10));
    }
}


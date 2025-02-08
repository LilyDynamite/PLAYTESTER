using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //glitch??
    public float glitchFrequency = 0.5f;
    public float glitchRestTime = 1.5f;

    public float gameDuration = 30f;

    public TMP_Text scoreText;
    public int scorePoints;

    //timer text
    public TMP_Text timerText;
    private bool gameOver = false;

    //glitches - through frequency will show glitches 
    //have problem i dont know how to add the prefab sprite renderer :(
   // public SpriteRenderer cupcakeRenderer;
    //public Color glitchColor = new Color(255f, 0f, 0f, 1f); // aMgenta
    //private Color normalcupcakeColor;
    //public GameObject prefabCupcake;


    void Start()
    {
        //normalcupcakeColor = cupcakeRenderer.color;
        timerText.text = "Time Left: " + gameDuration.ToString();
        StartCoroutine(StartTimer());
        //StartCoroutine(GlitchCheckRoutine());

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator StartTimer()
    {
        float timer = gameDuration; //timer

        while (timer > 0 && !gameOver)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time Left: " + Mathf.RoundToInt(timer);
            yield return null;
        }

        GameOver(); // game over 
    }

    void GameOver()
    {
        gameOver = true;
        timerText.text = "Game Stop!";
        Time.timeScale = 0f; // Stop everything
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

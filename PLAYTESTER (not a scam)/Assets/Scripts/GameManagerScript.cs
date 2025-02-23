using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //The game manager will be in charge of progressing the game and calling any UI functions it needs

    public int day; // Tracks which day it is (1 to 3)
    public int minigamesPlayed; // RESETS EACH DAY, tracks how many minigames have been completed (0 to 3)
    public int HP; //Tracks the current HP (0 to 100)
    public bool EMPHappened;
    
    private GameObject Bar;
    public AudioSource MusicPlayer;
    public AudioSource GlitchMusicPlayer; // Both musicPlayer and glitchMusicPlayer will play, but one will be muted depending on glitch frequency
    private GameObject UIController;

    //All audio tracks (sound effects are handled in their minigames but the minigame music is
    //all handled in this script
    public AudioClip mainTrack;
    public AudioClip cupcakeTrack;
    public AudioClip coinTrack;
    public AudioClip duckTrack;
    public AudioClip glitchedCoinTrack;
    public bool isGlitchActive = false;
    public float glitchDuration = 1.0f;
    private float glitchCooldown = 1f; // Cooldown for glitch check (1 second)
    private float glitchCooldownTimer = 0f; // Timer for cooldown

    //All news text slots
    private TMPro.TextMeshProUGUI ValText1;
    private TMPro.TextMeshProUGUI ValText2;
    private TMPro.TextMeshProUGUI LexaText1;
    private TMPro.TextMeshProUGUI LexaText2;
    private TMPro.TextMeshProUGUI CleeText1;
    private TMPro.TextMeshProUGUI CleeText2;

    // Reference to coin runner minigame manager
    public MinigameManager coinMinigameManager;

    // Start is called before the first frame update
    void Start()
    {
        //set the variables
        day = 1;
        minigamesPlayed = 0;
        HP = 100;
        EMPHappened = false;

        //get the objects
        UIController = GameObject.Find("UI Controller");
        Bar = GameObject.Find("Health Bar");
        MusicPlayer = GetComponent<AudioSource>();

        //Set the audio stuff
        MusicPlayer.clip = mainTrack;
        MusicPlayer.loop = true;
        MusicPlayer.Play();

        // Set up glitched music player
        if (GlitchMusicPlayer == null)
        {
            GlitchMusicPlayer = gameObject.AddComponent<AudioSource>();
        }
        GlitchMusicPlayer.loop = true;
        GlitchMusicPlayer.mute = true;

        //Get the news article text stuff
        ValText1 = GameObject.Find("Val Text 1").GetComponent<TMPro.TextMeshProUGUI>();
        ValText2 = GameObject.Find("Val Text 2").GetComponent<TMPro.TextMeshProUGUI>();
        LexaText1 = GameObject.Find("Lexa Text 1").GetComponent<TMPro.TextMeshProUGUI>();
        LexaText2 = GameObject.Find("Lexa Text 2").GetComponent<TMPro.TextMeshProUGUI>();
        CleeText1 = GameObject.Find("Clee Text 1").GetComponent<TMPro.TextMeshProUGUI>();
        CleeText2 = GameObject.Find("Clee Text 2").GetComponent<TMPro.TextMeshProUGUI>();

        // DEBUG: make a popup appear
        UIController.GetComponent<ComputerUIScript>().TriggerPopup(new Vector3(0,0,-2), "This is a popup! It can also be closed");


        // Slow down the title screen animation, news app captcha button
        GameObject.Find("PlayTesterAnimation").GetComponent<Animator>().speed = 0.5f;
        GameObject.Find("News App Captcha Button").GetComponent<Animator>().speed = 0.5f;

        //Update news articles
        UpdateNews();
    }

    // Update is called once per frame
    void Update()
    {
        // coin runner audio control specifically for now
        if (minigamesPlayed == 1 && !coinMinigameManager.IsGameOver()) // add && day == 2
        {
            glitchCooldownTimer -= Time.deltaTime;

            // If the cooldown timer reaches 0, check for glitch chance
            if (glitchCooldownTimer <= 0f)
            {
                // Reset the cooldown timer
                glitchCooldownTimer = glitchCooldown;

                if (!isGlitchActive && Random.value < coinMinigameManager.glitchFreq) // 
                {
                    Debug.Log("Starting coin glitch");
                    StartCoroutine(HandleCoinMinigameAudio());
                }
            }
        }
    }

    // Advances the minigame counter and changes the HP bar
    public void CompletedMinigame(int scoreChange)
    {

        minigamesPlayed += 1;

        HP += scoreChange;

        //Make sure HP is within bounds
        if (HP > 100)
        {
            HP = 100;
        }
        if (HP < 1)
        {
            HP = 1;
        }

        //Update the HP bar
        Bar.GetComponent<HealthBar>().SetHealth(HP);

        //Start playing the UI music again
        MusicPlayer.clip = mainTrack;
        MusicPlayer.Play();

    }

    //Called by pressing clock out button
    public void CompletedDay()
    {
        day += 1;
        minigamesPlayed = 0;

        //Update news articles
        UpdateNews();
    }

    public int GetHP()
    {
        return HP;
    }

    public int GetMinigamesPlayed()
    {
        return minigamesPlayed;
    }

    public int GetDay()
    {
        return day;
    }

    public void StartedMinigame()
    {
        
        //start playing the music for the minigame being played

        if(minigamesPlayed == 0)
        {
            //playing cupcake
            MusicPlayer.clip = cupcakeTrack;
           
        }
        else if (minigamesPlayed == 1)
        {
            //playing coin
            MusicPlayer.clip = coinTrack;
            GlitchMusicPlayer.clip = glitchedCoinTrack;
            GlitchMusicPlayer.mute = false;
           
        }
        else
        {
            //playing duck
            MusicPlayer.clip = duckTrack;
            
        }
        MusicPlayer.Play();
        GlitchMusicPlayer.Play();
    }

    // Audio glitch handling for coin runner minigame
    private IEnumerator HandleCoinMinigameAudio()
    {
        isGlitchActive = true;

        MusicPlayer.mute = true;
        GlitchMusicPlayer.mute = false;

        yield return new WaitForSeconds(glitchDuration); // glitch lasts this long (ie 1 second)

        MusicPlayer.mute = false;
        GlitchMusicPlayer.mute = true;

        isGlitchActive = false;

    }
    //Updates the news articles
    public void UpdateNews()
    {
        //check what day it is, HP, how many mingames have been played, etc, and
        //put in all the needed articles

        if(day == 1)
        {
            //Display day 1 articles
            ValText1.SetText("This is tester text.\nWow a new line!");

            ValText2.SetText("Yay I love day 1 and I love my wife");
        }
        else if (day == 2)
        {
            if(!EMPHappened)
            {
                //Display day 2 before emp articles
                ValText1.SetText("This is still tester text.\nWow a new line!");
                ValText2.SetText("Yay I love day 2 and I still love my wife");
            }
            else
            {
                // FIXME: Not being updated!!
                //Display day 2 after emp articles
                ValText1.SetText("This is still tester text.\nWow a new line! \nOh no the EMP happened");

            }
        }
        else
        {
            if(HP > 50)
            {
                //Display day 3 rebellion ending articles
            }
            else
            {
                //Display day 3 complicit ending articles
            }
        }


        return;
    }
}

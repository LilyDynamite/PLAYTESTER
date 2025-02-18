﻿using System.Collections;
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
    private GameObject UIController;

    //All audio tracks (sound effects are handled in their minigames but the minigame music is
    //all handled in this script
    public AudioClip mainTrack;
    public AudioClip cupcakeTrack;
    public AudioClip coinTrack;
    public AudioClip duckTrack;

    //All news text slots
    private TMPro.TextMeshProUGUI ValText1;
    private TMPro.TextMeshProUGUI ValText2;
    private TMPro.TextMeshProUGUI LexaText1;
    private TMPro.TextMeshProUGUI LexaText2;
    private TMPro.TextMeshProUGUI CleeText1;
    private TMPro.TextMeshProUGUI CleeText2;

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

        //Get the news article text stuff
        ValText1 = GameObject.Find("Val Text 1").GetComponent<TMPro.TextMeshProUGUI>();
        ValText2 = GameObject.Find("Val Text 2").GetComponent<TMPro.TextMeshProUGUI>();
        LexaText1 = GameObject.Find("Lexa Text 1").GetComponent<TMPro.TextMeshProUGUI>();
        LexaText2 = GameObject.Find("Lexa Text 2").GetComponent<TMPro.TextMeshProUGUI>();
        CleeText1 = GameObject.Find("Clee Text 1").GetComponent<TMPro.TextMeshProUGUI>();
        CleeText2 = GameObject.Find("Clee Text 2").GetComponent<TMPro.TextMeshProUGUI>();



        // DEBUG: make a popup appear
        UIController.GetComponent<ComputerUIScript>().TriggerPopup(new Vector3(0,0,-2), "This is a popup! It can also be closed");


        //Slow down the title screen animation
        GameObject.Find("PlayTesterAnimation").GetComponent<Animator>().speed = 0.5f;

        //Update news articles
        UpdateNews();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
        else
        {
            //playing duck
            MusicPlayer.clip = duckTrack;
        }
        MusicPlayer.Play();
    }

    //Updates the news articles
    private void UpdateNews()
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
            if(EMPHappened)
            {
                //Display day 2 before emp articles
            }
            else
            {
            
                //Display day 2 after emp articles
            
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

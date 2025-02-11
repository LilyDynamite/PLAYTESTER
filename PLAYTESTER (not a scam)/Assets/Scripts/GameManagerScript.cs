using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //The game manager will be in charge of progressing the game and calling any UI functions it needs

    public int day; // Tracks which day it is (1 to 3)
    public int minigamesPlayed; // RESETS EACH DAY, tracks how many minigames have been completed (0 to 3)
    public int HP; //Tracks the current HP (0 to 100)
    
    private GameObject Bar;
    public AudioSource MusicPlayer;

    private GameObject UIController;

    //All audio tracks (sound effects are handled in their minigames but the minigame music is
    //all handled in this script
    public AudioClip mainTrack;
    public AudioClip cupcakeTrack;
    public AudioClip coinTrack;
    public AudioClip duckTrack;


    // Start is called before the first frame update
    void Start()
    {
        //set the variables
        day = 1;
        minigamesPlayed = 0;
        HP = 100;

        //get the objects
        UIController = GameObject.Find("UI Controller");
        Bar = GameObject.Find("Health Bar");
        MusicPlayer = GetComponent<AudioSource>();

        //Set the audio stuff
        MusicPlayer.clip = mainTrack;
        MusicPlayer.loop = true;
        MusicPlayer.Play();

        // DEBUG: make a popup appear
        UIController.GetComponent<ComputerUIScript>().TriggerPopup(new Vector3(0,0,-2), "This is a popup! It can also be closed");


        //Slow down the title screen animation
        GameObject.Find("PlayTesterAnimation").GetComponent<Animator>().speed = 0.5f;
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
}

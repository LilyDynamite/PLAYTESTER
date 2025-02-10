using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //The game manager will be in charge of progressing the game and calling any UI functions it needs

    public int day; // Tracks which day it is (1 to 3)
    public int minigamesPlayed; // RESETS EACH DAY, tracks how many minigames have been completed (0 to 3)
    public int HP; //Tracks the remaining HP (0 to 100)

    private GameObject UIController;

    // Start is called before the first frame update
    void Start()
    {
        //set the variables
        day = 1;
        minigamesPlayed = 0;
        HP = 100;

        //get the ui controller object
        UIController = GameObject.Find("UI Controller");

        // DEBUG: make a popup appear
        UIController.GetComponent<ComputerUIScript>().TriggerPopup(new Vector3(0,0,-2), "This is a popup! It can also be closed");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Advances the minigame counter and changes the HP bar
    public void CompletedMinigame(int scoreChange)
    {
        Debug.Log("Completed minigame was called");

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
}

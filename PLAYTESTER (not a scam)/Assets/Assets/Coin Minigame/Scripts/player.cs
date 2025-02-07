using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices.ComTypes;
using TMPro;
using UnityEngine;

public class player : MonoBehaviour
{
    public Transform[] lanes;
    private int curLane = 1; // middle lane
    public TMP_Text pointsText;
    private int points = 0;
    private AudioSource sfx;
    UnityEngine.Color colorOne = new UnityEngine.Color(1f, 1f, 0f, 1f); // yellow
    UnityEngine.Color colorTwo = new UnityEngine.Color(0.5f, 0f, 0.5f, 1f); // purple

    void Start()
    {
        sfx = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Detect up and down arrow keyboard input 
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }
    }

    void MoveUp()
    {
        if (curLane > 0) // Prevent out of bounds
        {
            curLane--;
            // transform.position = lanes[curLane].position;
            transform.position = new Vector3(lanes[curLane].position.x, lanes[curLane].position.y, -2);
        }
    }

    void MoveDown()
    {
        if (curLane < lanes.Length - 1) // Prevent out of bounds
        {
            curLane++;
            // transform.position = lanes[curLane].position;
            transform.position = new Vector3(lanes[curLane].position.x, lanes[curLane].position.y, -2);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("spawnable"))
        {
            SpriteRenderer spriteRenderer = other.GetComponent<SpriteRenderer>();

            if (spriteRenderer.color == colorOne) // The spawnable touched adds points
            {
                points += 1; 
            } else // Otherwise it is a spawnable that takes away points
            {
                points -= 1;
            }
            pointsText.text = "Points: " + points;
            
            if (sfx != null)
            {
                Debug.Log("play!"); 
                sfx.Play(); // Play sound effect
            }

            Destroy(other.gameObject); 
        }
    }
}

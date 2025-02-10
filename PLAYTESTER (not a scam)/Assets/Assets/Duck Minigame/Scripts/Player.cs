using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SpriteRenderer crosshairSprite;
    public LayerMask interactableLayer;
    public DuckGameManager duckGameManager;

    void Start()
    {
        duckGameManager = GameObject.Find("DuckGameManager").GetComponent<DuckGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (duckGameManager.gameOver == false)
        { 
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            crosshairSprite.transform.position = mousePos;

            if (Input.GetMouseButtonDown(0)) // Left click
            {
                DetectObjectUnderCrosshair(mousePos);
            }
        }
    }

    void DetectObjectUnderCrosshair(Vector2 mousePos)
    {
        Collider2D hit = Physics2D.OverlapPoint(mousePos, interactableLayer);
        if (hit != null)
        {
            // If the crosshair is over something, perform actions (like hitting a duck or trash)
            if (hit.CompareTag("Duck"))
            {
                // Add 1 point for duck
                duckGameManager.AddPoints(1);
                Debug.Log("Hit Duck +1 Point");
                Destroy(hit.gameObject); // Destroy duck after hit
            }
            else if (hit.CompareTag("Trash"))
            {
                // Subtract 1 point for trash
                duckGameManager.AddPoints(-1);
                Debug.Log("Hit Trash -1 Point");
                Destroy(hit.gameObject); // Destroy trash after hit
            }
        }
    }
}

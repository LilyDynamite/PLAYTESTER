using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnMg : MonoBehaviour
{
    public GameObject spawnable;
    public Transform[] spawnPos;
    public float spawnInterval = 1.5f;
    public float speed = 2f;
    Color colorOne = new Color(1f, 1f, 0f, 1f); // Yellow
    Color colorTwo = new Color(0.5f, 0f, 0.5f, 1f); // Purple

    private MinigameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("MinigameManager").GetComponent<MinigameManager>();
    }

    public void SpawnMgStart()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        Debug.Log("entered the spawn objects call at all");
        while (!gameManager.IsGameOver())
        {
            Debug.Log("entered while loop");
            yield return new WaitForSeconds(spawnInterval); // Make spawnables every x seconds

            int slotOne = Random.Range(0, spawnPos.Length);
            int slotTwo;
            do
            {
                slotTwo = Random.Range(0, spawnPos.Length);
            } while (slotTwo == slotOne); // Ensure the slots are different

            Spawn(spawnPos[slotOne], colorOne); // This will be the good coin
            Spawn(spawnPos[slotTwo], colorTwo); // This will be the bad coin
        }
    }

    void Spawn(Transform spawnPos, Color color)
    {
        GameObject newSpawnable = Instantiate(spawnable, spawnPos.position, Quaternion.identity); // Make the spawnable

        SpriteRenderer spriteRenderer = newSpawnable.GetComponent<SpriteRenderer>(); // Access the spawnable's color
        spriteRenderer.color = color; // Change the spawnable's color
    }
}

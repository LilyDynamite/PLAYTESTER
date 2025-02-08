using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{ 
    public float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;
 
    // Start is called before the first frame update
    void Start()
    {
        speed=Random.Range(minSpeed,maxSpeed); //randomize speed of people
  
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if it hits the walls of the game, move other way
        if (other.gameObject.tag == "Boundary")
        {
            speed *= -1;
        }
    }
}

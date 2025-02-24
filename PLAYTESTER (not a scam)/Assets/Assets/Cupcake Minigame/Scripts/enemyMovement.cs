using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{ 
    public float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;

    SpriteRenderer spriteRenderer;
 
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (other.gameObject.tag == "LBoundary")
        {
            speed *= -1;
            spriteRenderer.flipX= false;

        }

        if (other.gameObject.tag == "RBoundary")
        {
            speed *= -1;
            spriteRenderer.flipX = true;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CupcakeMovement : MonoBehaviour
{
    
    [SerializeField] public float speed = 0.7f;
    //I cant for some reason put the timer in the text idk if it is because it is a prefab or im dumb 
    //public TMP_Text scoreText; //score text
    //public int scorePoints=0;

    public GameManager GameManager;


    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake() 
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime); //cupcake goes down
    }

    private void OnTriggerEnter2D(Collider2D collision){  
    // collider check if it hits the ground or people
        if(collision.gameObject.tag == "Ground") 
        {  
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Person")
        {
            GameManager.AddPoints(1);
            //scorePoints += 1;
            //scoreText.text = "Score:"+scorePoints.ToString();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

    
    }


}

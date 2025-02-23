using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CupcakeMovement : MonoBehaviour
{
    
    [SerializeField] public float speed = 6f;
    // Audio
    public AudioSource sfx;
    public AudioClip sfxYay;
    public AudioClip sfxGlitchedYay;
    //I cant for some reason put the timer in the text idk if it is because it is a prefab or im dumb 
    //public TMP_Text scoreText; //score text
    //public int scorePoints=0;

    private CupcakeGameManager GameManager;


    // Start is called before the first frame update
    void Start()
    {
    
    }

    void Awake() 
    {
        sfx = GetComponent<AudioSource>();
        GameManager = GameObject.Find("CupcakeGameManager").GetComponent<CupcakeGameManager>();
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
            if (sfxYay != null)
            {
                if (!GameManager.isGlitch)
                {
                    sfx.clip = sfxYay;
                    sfx.Play();
                } else // means the glitch is occuring
                {
                    sfx.clip = sfxGlitchedYay;
                    sfx.Play();
                }
            }
            GameManager.AddPoints(1);
            
            //decrease total enemies on screen
            GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().KilledEnemy();
            Destroy(collision.gameObject);
            //Destroy(gameObject);

            
        }

    
    }

}

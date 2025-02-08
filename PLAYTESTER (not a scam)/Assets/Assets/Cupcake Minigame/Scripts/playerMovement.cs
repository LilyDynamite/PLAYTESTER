using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public GameObject cupcake;
    public float speed = 1.85f;
    private float waitTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        //player input
        waitTime-= Time.deltaTime;
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            transform.position+= Vector3.left*Time.deltaTime*speed;
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            transform.position+= Vector3.right*Time.deltaTime*speed;
        }
        if(Input.GetKeyDown(KeyCode.Space) && waitTime<=0.0f){
            Instantiate(cupcake, transform.position,Quaternion.identity);
            waitTime = 1.5f;
        }
    }
}

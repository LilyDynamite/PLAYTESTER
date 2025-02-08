using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject person;

    [SerializeField] private int numEnemies;
    private int currentNumEnemies;
    private float waitTime;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //spawn enemies after each spawn wait time with a max amount of people
        waitTime-=Time.deltaTime;
        if (waitTime<=0 && currentNumEnemies<numEnemies){
            Instantiate(person,transform.position,Quaternion.identity);
            setSpawnTime();
            currentNumEnemies++;
        }
    }

    void setSpawnTime(){
        waitTime=Random.Range(0.5f,3.0f);
    }
}

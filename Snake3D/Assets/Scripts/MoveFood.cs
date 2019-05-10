using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFood : MonoBehaviour
{
    public float speed = 5;
    public Transform[] moveSpots;
    private int randomSpots;
    


    void Start()
    {
        randomSpots = Random.Range(0, moveSpots.Length);
    }

    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpots].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, moveSpots[randomSpots].position) < 0.2f)        
            randomSpots = Random.Range(0, moveSpots.Length);
        
    }

}

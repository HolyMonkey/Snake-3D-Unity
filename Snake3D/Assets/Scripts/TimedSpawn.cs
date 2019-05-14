using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpawn : MonoBehaviour
{
    public GameObject Food;
    public bool StopSpawning = false;
    public float SpawnTime;
    public float SpawnDelay;
    public int CountFood = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", SpawnTime, SpawnDelay);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnObject()
    {
        /*
        Instantiate(Food, transform.position, Quaternion.identity);
        CountFood += 1;
        */

        if (CountFood < 10)
        {
            Instantiate(Food, transform.position, Quaternion.identity);

            CountFood += 1;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barriermoving : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float xMax;
    [SerializeField] private float xMin;

    private void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
        if (transform.position.x > xMax)
        {
            speed = -1f;
        }
        if (transform.position.x < xMin)
        {
            speed = 1f;
        }
    }

        
  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier4moving : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Start()
    {
        
    }

    private void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
        if (transform.position.x > 14)
        {
            speed = -1f;
        }
        if (transform.position.x < 10.4)
        {
            speed = 1f;
        }
    }

        
  
}

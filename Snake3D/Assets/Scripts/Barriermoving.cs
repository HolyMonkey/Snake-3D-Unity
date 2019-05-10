using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barriermoving : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float border1;
    [SerializeField] private float border2;
    private void Start()
    {
        
    }

    private void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
        if (transform.position.x > border1)
        {
            speed = -1f;
        }
        if (transform.position.x < border2)
        {
            speed = 1f;
        }
    }

        
  
}

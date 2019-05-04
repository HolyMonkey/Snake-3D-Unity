using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public float speed;
    void Start()
    {
        speed = 0.03f;
    }

    // Start is called before the first frame update
    void Update()
    {
        transform.Translate(0, 0, speed);
        if (transform.position.x > 13.5)
        {
            speed = -0.03f;
        }
        if (transform.position.x < 10.9)
        {
            speed = 0.03f;
        }
    }

    // Update is called once per frame
  
}

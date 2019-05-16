using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMove : MonoBehaviour
{    
    private float _delaytimer;
    private Vector3 _pos;
    

    private void Start()
    {        
        getNewPosition(); // Get initial targetpos
    }

    private void Update()
    {
        _delaytimer += Time.deltaTime;
                
        if (_delaytimer > 1) // Time to wait 
        {
            getNewPosition(); // Get new position every 5 second
            _delaytimer = 0f; // Reset timer
        }        

        transform.position = Vector3.MoveTowards(transform.position, _pos, .1f);
    }

    private void getNewPosition()
    {
        float x = Random.Range(-17, 18); // Left border position, Right border position
        float z = Random.Range(2, 38); // Bottom border position, Top border position

        _pos = new Vector3(x, 0, z);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Border")
        {            
            getNewPosition();
        }        
    }
}

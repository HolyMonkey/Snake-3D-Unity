using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFor : MonoBehaviour
{
    [SerializeField] private float _speed;    
    [SerializeField] private Transform[] _spots;
    private int _currentSpot;

    private void Start()
    {
        _currentSpot = Random.Range(0, _spots.Length);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _spots[_currentSpot].position, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _spots[_currentSpot].position) < 0.2f)
            _currentSpot = Random.Range(0, _spots.Length);
    }
}

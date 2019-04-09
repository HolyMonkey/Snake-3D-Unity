using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    private GameObject foodPref;

    [SerializeField]
    private int _maxFood;

    private GameObject _curFood;

    private float _xMinSize = -18.3f;
    private float _xMaxSize = 18.3f;

    private float _zMinSize = 1.5f;
    private float _zMaxSize = 38.3f;

    private Vector3 _curPos;
    [HideInInspector]
    public int createFood = 0;

    private void Start()
    {
        for (int i = 0; i < _maxFood; i++)
        {            
            InstFood();
        }        
    }

    private void Update()
    {
        if (_maxFood  > createFood)
        {
            InstFood();
        }
    }

    private void InstFood ()
    {
        RandomPos();
        _curFood = Instantiate(foodPref, _curPos, Quaternion.identity) as GameObject;
        createFood++;
    }

    private void RandomPos()
    {
        _curPos = new Vector3(Random.Range(_xMinSize,_xMaxSize),0.5f,Random.Range(_zMinSize,_zMaxSize));
    }

}

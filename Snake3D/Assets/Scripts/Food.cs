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

    private void Start()
    {
        for (int i = 0; i < _maxFood; i++)
        {
            InstFood();
        }
    }

    private void Update()
    {

    }

    private void InstFood()
    {
        RandomPos();
        _curFood = Instantiate(foodPref, _curPos, Quaternion.identity) as GameObject;
    }

    private void RandomPos()
    {
        _curPos = new Vector3(Random.Range(_xMinSize, _xMaxSize), 0.5f, Random.Range(_zMinSize, _zMaxSize));
    }

    public void Eat(int CreateFood)
    {
        InstFood();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPlacer : MonoBehaviour
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

    private void Start()
    {
        for (int i = 0; i < _maxFood; i++)
        {
            InstantiateFood();
        }
    }

    private void InstantiateFood()
    {
        _curFood = Instantiate(foodPref, new Vector3(Random.Range(_xMinSize, _xMaxSize), 0.5f, Random.Range(_zMinSize, _zMaxSize)), Quaternion.identity) as GameObject;
    }

    public void Eat()
    {
        InstantiateFood();
    }
}

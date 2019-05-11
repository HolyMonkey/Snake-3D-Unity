using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    public GameObject prefabFood;
    private GameObject cloneFood;
    private void Start()
    {
        spawnFoodObject();
    }
    public void spawnFoodObject()
    {
        cloneFood = Instantiate(prefabFood, transform);
        Vector3 posClone = new Vector3(Random.Range(-16.71f, 16.71f),1, Random.Range(-17.9f, 17.9f));
        cloneFood.transform.localPosition = posClone;
    }
}

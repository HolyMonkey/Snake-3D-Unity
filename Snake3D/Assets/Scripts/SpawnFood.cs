using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    public GameObject PrefabFood;
    public Transform ObjectTopRightBorder, ObjectBotLeftBorder;
    private GameObject _cloneFood;

    private void Start()
    {
        SpawnFoodObject();
    }

    public void SpawnFoodObject()
    {
        _cloneFood = Instantiate(PrefabFood, transform);
        Vector3 positionClone = new Vector3(Random.Range(ObjectBotLeftBorder.localPosition.x, ObjectTopRightBorder.localPosition.x),1,Random.Range(ObjectBotLeftBorder.localPosition.z, ObjectTopRightBorder.localPosition.z));
        _cloneFood.transform.localPosition = positionClone;
    }
}

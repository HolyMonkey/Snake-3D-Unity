using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    public GameObject PrefabFood;
    public Transform ObjectTopRightBorder, ObjectBotLeftBorder;
    private GameObject _cloneFood;
    private Renderer _renderer;
    private float _alpha;
    public bool isCountdown = false;
    [Range(0.001f, 0.1f)]
    public float CountdownSpeed;

    private void Start()
    {
        SpawnFoodObject();
    }

    public void SpawnFoodObject()
    {
        _alpha = 1f;
        _cloneFood = Instantiate(PrefabFood, transform);
        Vector3 positionClone = new Vector3(Random.Range(ObjectBotLeftBorder.localPosition.x, ObjectTopRightBorder.localPosition.x),1,Random.Range(ObjectBotLeftBorder.localPosition.z, ObjectTopRightBorder.localPosition.z));
        _cloneFood.transform.localPosition = positionClone;

        _renderer = _cloneFood.GetComponent<Renderer>();
    }

    public void Update() {
        if(isCountdown) {
            if (_alpha > 0) {
                _renderer.material.color = new Color(0, 1f, 0, _alpha);
                _alpha -= CountdownSpeed;
            } else {
                Destroy(_cloneFood);
                SpawnFoodObject();
            }
        }
    }
}
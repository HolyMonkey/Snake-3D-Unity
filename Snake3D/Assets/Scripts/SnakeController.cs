using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnakeController : MonoBehaviour
{
    public List<Transform> Tails;
    [Range(0,3)]
    public float BonesDictance;
    public GameObject BonePrefab;
    [Range(0,4)]
    public float Speed;
    private Transform _transform;

    public UnityEvent OnEat;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        MoveSnake(_transform.position + _transform.forward * Speed);
        float angle = Input.GetAxis("Horizontal")*4;
        _transform.Rotate(0, angle, 0);
    }   

    private void MoveSnake(Vector3 newPosition)
    {
        float sqrDistance = BonesDictance * BonesDictance;
        Vector3 previousPosition = _transform.position;

        foreach (var bone in Tails)
        {
            if((bone.position - previousPosition).sqrMagnitude > sqrDistance)
            {
                var temp = bone.position;
                bone.position = previousPosition;
                previousPosition = temp;
            }
            else
            {
                break;
            }
        }


        _transform.position = newPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            Destroy(collision.gameObject);
            var bone = Instantiate(BonePrefab);
            Tails.Add(bone.transform);
            if(OnEat != null)
            {
                OnEat.Invoke();
            }
        }
    }
}

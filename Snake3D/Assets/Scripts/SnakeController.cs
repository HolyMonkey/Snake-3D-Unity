using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{
    public List<Transform> Tails;
    [Range(0,3)]
    public float BonesDictance;
    public GameObject BonePrefab;
    [Range(0,4)]
    public float Speed;
    private Transform _transform;
    public GameObject Bone;
    public UnityEvent OnEat;

    private void Start()
    {
        _transform = GetComponent<Transform>();

    }
    private void Update()
    {
        MoveSnake(_transform.position + _transform.forward * Speed);
        float angle = Input.GetAxis("Horizontal")*6;
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
        if (collision.gameObject.tag == "Barrier")
        {            
            int i = Tails.Count;
            if (i >=2)
            {
                Destroy(Tails[i - 1].gameObject);
                Tails.RemoveAt(i - 1);
                Speed *= 0.9f;
                _transform.position = new Vector3(-2.76f, 1f, 13.48f);
                _transform.rotation = Quaternion.Euler(0, 0, 0);
                for (int j = 0; j <= i-2; j++)
                {
                    Tails[j].gameObject.transform.position = new Vector3(-2.76f, 1f, 13.48f - BonesDictance*(j));
                }
                
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }

        if (collision.gameObject.tag == "Border")
        {
            SceneManager.LoadScene(0);
        }

        if (collision.gameObject.tag == "Food")
        {
            Destroy(collision.gameObject);
            var bone = Instantiate(BonePrefab);
            Tails.Add(bone.transform);
            Speed *= 1.1f;
            if(OnEat != null)
            {
                OnEat.Invoke();
            }
        }
                    
    }
}

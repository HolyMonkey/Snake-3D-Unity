using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SnakeController : MonoBehaviour
{
    public List<Transform> Tails;

    [Range(0, 3)]
    public float BonesDictance;
    public GameObject BonePrefab;

    [Range(0, 4)]
    public float Speed;

    [Range(4, 8)]
    public float RotationSpeed;

    public GameObject Bone;
    public UnityEvent OnEat;

    [SerializeField]
    private Text ScoreText;
    [SerializeField]
    private GameObject _gameController;

    private Transform _transform;
    private int _eatedFoodCount;
    private Food _eat;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _eat = _gameController.GetComponent<Food>();
    }

    private void Update()
    {
        ScoreText.text = _eatedFoodCount.ToString();
        MoveSnake(_transform.position + _transform.forward * Speed);
        float angle = Input.GetAxis("Horizontal") * RotationSpeed;
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

    private void OnHitBarrier()
    {
        if (Tails.Count >= 2)
        {
            Destroy(Tails[Tails.Count - 1].gameObject);
            Tails.RemoveAt(Tails.Count - 1);
            Speed *= 0.9f;
            _transform.position = new Vector3(-2.76f, 1f, 13.48f);
            _transform.rotation = Quaternion.Euler(0, 0, 0);

            for (int j = 0; j <= Tails.Count - 1; j++)
            {
                Tails[j].gameObject.transform.position = new Vector3(-2.76f, 1f, 13.48f - BonesDictance * j);
            }
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            OnHitBarrier();
        }

        if (collision.gameObject.tag == "Border")
        {
            SceneManager.LoadScene(0);
        }

        if (collision.gameObject.tag == "Food")
        {
            Destroy(collision.gameObject);

            _eat.Eat();
            _eatedFoodCount++;

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

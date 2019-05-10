using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class SnakeController : MonoBehaviour
{

    public List<Transform> Tails;
    [Range(0, 3)]
    public float BonesDictance;
    public GameObject BonePrefab;
    [Range(0, 4)]
    public float Speed;
    [Range(6, 16)]
    public float jumpHight = 8f;
    [Range(5, 30)]
    public float gravity = 20f;
    public bool isLevelFly = false;
    public bool isLevelJump = false;

    private bool _isJump = false;
    private float _startPosY;
    [Range(4, 8)]
    public float rotationSpeed;
    public UnityEvent OnEat;

    private GameObject _bone;
    private Transform _transform;

    private Vector3 _moveDirection = Vector3.zero;
    private int _countBone = 0;

    private float _angle = 0.0f;
    private float _angleUpDown = 0.0f;
    private float _angleRound = 0.0f;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _startPosY = _transform.position.y;
        _bone = new GameObject();
    }

    private void Update()
    {
        MoveSnake(_transform.position + _transform.forward * Speed);
        _angle = Input.GetAxis("Horizontal") * rotationSpeed;

        if (isLevelFly)
        {
            FlySnake();
        }

        if (isLevelJump)
        {
            JumpSnake();
        }

        _transform.Rotate(_angleUpDown, _angle, _angleRound);
        EatBone();
    }

    private void FlySnake()
    {
        _angleUpDown = Input.GetAxis("Vertical") * rotationSpeed;

        if (Input.GetKey(KeyCode.Q))
        {
            _angleRound = 0.3f * rotationSpeed;
        }

        if (Input.GetKey(KeyCode.E))
        {
            _angleRound = -0.3f * rotationSpeed;
        }
    }

    private void EatBone()
    {
        RaycastHit hit;
        Vector3 fwd = _transform.forward;

        if (Physics.Raycast(_transform.position, fwd, out hit, 0.1f))
        {
            if (hit.collider.tag == "Bone")
            {
                OnHitBarrier();
            }
        }
    }

    private void JumpSnake()
    {
        if (_transform.position.y <= _startPosY)
        {
            if (Input.GetButton("Jump"))
            {
                _isJump = true;
            }
        }

        _moveDirection = _transform.position;
        if (_isJump == true && _moveDirection.y < jumpHight)
        {
            _moveDirection.y += gravity * Time.deltaTime;
        }

        if (_moveDirection.y >= jumpHight)
        {
            _isJump = false;
        }

        if (_isJump == false && _moveDirection.y > _startPosY)
        {
            _moveDirection.y -= gravity * Time.deltaTime;
        }

        if (_moveDirection.y < _startPosY)
        {
            _moveDirection.y = _startPosY;
        }

        _transform.position = _moveDirection;
    }

    private void MoveSnake(Vector3 newPosition)
    {
        float sqrDistance = BonesDictance * BonesDictance;
        Vector3 previousPosition = _transform.position;
        _bone.transform.position = _transform.position;
        _bone.transform.rotation = _transform.rotation;

        foreach (var bone in Tails)
        {
            if ((bone.position - previousPosition).sqrMagnitude > sqrDistance)
            {

                var tempP = bone.position;
                var tempR = bone.rotation;
                bone.position = _bone.transform.position;
                bone.rotation = _bone.transform.rotation;
                previousPosition = tempP;
                _bone.transform.position = tempP;
                _bone.transform.rotation = tempR;
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
            var bone = Instantiate(BonePrefab);
            bone.transform.position = _bone.transform.position;
            bone.transform.rotation = _bone.transform.rotation;
            Tails.Add(bone.transform);
            Speed *= 1.1f;
            if (OnEat != null)
            {
                OnEat.Invoke();
            }
        }
    }
}

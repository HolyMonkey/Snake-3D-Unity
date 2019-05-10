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
    [Range(0, 4)]
    public float Speed;
    [Range(6, 16)]
    public float jumpHight = 8;
    public float gravity = 20f;
    private bool isJump = false;
    private float startPosY;
    [Range(4, 8)]
    public float rotationSpeed;
    private GameObject _bone;
    public UnityEvent OnEat;
    private Transform _transform;

    private Vector3 moveDirection = Vector3.zero;
    private int countBone = 0;



    private void Start()
    {
        _transform = GetComponent<Transform>();
        startPosY = _transform.position.y;
        _bone = new GameObject();
    }

    private void Update()
    {
        MoveSnake(_transform.position + _transform.forward * Speed);
        float angle = Input.GetAxis("Horizontal") * rotationSpeed;
        _transform.Rotate(0, angle, 0);


        if (_transform.position.y <= startPosY)
        {
            if (Input.GetButton("Jump"))
            {

                isJump = true;
            }
        }
        JumpSnake();
        EatBone();
    }

    private void FixedUpdate()
    {
      
    }

    private void EatBone()
    {

        RaycastHit hit;
        Vector3 fwd = _transform.TransformDirection(Vector3.forward);


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
        moveDirection = _transform.position;
        if (isJump == true && moveDirection.y < jumpHight)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }
        if (moveDirection.y >= jumpHight)
        {
            isJump = false;
        }
        if (isJump == false && moveDirection.y > startPosY)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        if (moveDirection.y < startPosY)
        {
            moveDirection.y = startPosY;
        }
        _transform.position = moveDirection;
    }

    private void MoveSnake(Vector3 newPosition)
    {
        float sqrDistance = BonesDictance * BonesDictance;
        Vector3 previousPosition = _transform.position;

       // GameObject test= new GameObject();
        
        foreach (var bone in Tails)
        {
            if (countBone == 0)
            {

                if ((bone.position - previousPosition).sqrMagnitude > sqrDistance)
                {
                    var temp = bone.position;
                    bone.position = previousPosition;
                    bone.rotation = _transform.rotation;
                    previousPosition = temp;

                    _bone.transform.position = bone.position;
                    _bone.transform.rotation = bone.rotation;
                }
                else
                {
                    break;
                }
            }
            else
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
            countBone++;
        }
        countBone = 0;
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
            Tails.Add(bone.transform);
            Speed *= 1.1f;
            if(OnEat != null)
            {
                OnEat.Invoke();
            }
        }
                    
    }
}

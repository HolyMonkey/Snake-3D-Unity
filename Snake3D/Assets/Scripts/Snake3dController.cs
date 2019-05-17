using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Snake3dController : MonoBehaviour
{

    public GameObject BonePrefab;
    public GameObject FoodRedPrefab;
    public GameObject FoodGreenPrefab;
    public GameObject FoodBluePrefab;

    private List<Transform> Tails = new List<Transform>();
    private List<Transform> FoodRed = new List<Transform>();
    private List<Transform> FoodGreen = new List<Transform>();
    private List<Transform> FoodBlue = new List<Transform>();


    [Header("Foods")]
    public int foodGreen = 20;
    public int foodRed = 30;
    public int foodBlue = 10;

    [Header("GameModeFly")]
    public bool isLevelFly = false;

    public float moveSpeed = 0.3f;
    public float maxMoveSpeed = 0.1f;

    private float _angleRound;
    private bool isRotate = false;
    private bool isBarrierUp = false;
    private bool isBarrierDown = false;
    private bool isBarrierForward = false;

  
    private GameObject _bone;

    GameObject score;
    private int _score = 0;
    GameObject life;
    private int _life = 3;
    GameObject jump;
    private int _jump = 1;
    public float jumpTime = 1f;
    public float jumpSpeed = 0.02f;
    private bool isJumpSnake = false;

    GameObject timer;
    private int _timeToLost = 30;

    public string[] tags = { "FoodRed", "FoodGreen", "FoodBlue" };
    public int[] scorePoint = { 10, 50, 100 };
    public int[] timePoint = { 5, 20, 10 };

    void Start()
    {
        score = GameObject.Find("Score");
        life = GameObject.Find("Life");
        jump = GameObject.Find("Jump");
        timer = GameObject.Find("Timer");
        _bone = new GameObject();
        StartCoroutine(MoveSnake());
        StartCoroutine(TimeGame());
        MakeFood(foodRed, foodGreen, foodBlue);

    }
  
    void Update()
    {
        if (isLevelFly)
        {
            FlySnake();
            DrowUI();
        }
    }

    private void DrowUI()
    {
        score.transform.GetComponent<Text>().text = "SCORE: " + _score.ToString();
        life.transform.GetComponent<Text>().text = "LIFE: " + _life.ToString();
        jump.transform.GetComponent<Text>().text = "JUMP: " + _jump.ToString();
        timer.transform.GetComponent<Text>().text = "TIME: " + _timeToLost.ToString();
    }

    public void MakeFood(int red = 0, int green = 0, int blue = 0)
    {
        //не додумал еще как сделать рефакторинг
        for (int i = 0; i < red; i++)
        {
            Vector3 redPosition = new Vector3(Random.Range(-24, 24), Random.Range(-24, 24), Random.Range(2, 48));
            var food = Instantiate(FoodRedPrefab, redPosition, Quaternion.identity);
            FoodRed.Add(food.transform);
        }
        for (int i = 0; i < green; i++)
        {
            Vector3 greenPosition = new Vector3(Random.Range(-24, 24), Random.Range(-24, 24), Random.Range(2, 48));
            var food = Instantiate(FoodGreenPrefab, greenPosition, Quaternion.identity);
            FoodGreen.Add(food.transform);
        }
        for (int i = 0; i < blue; i++)
        {
            Vector3 bluePosition = new Vector3(Random.Range(-24, 24), Random.Range(-24, 24), Random.Range(2, 48));
            var food = Instantiate(FoodBluePrefab, bluePosition, Quaternion.identity);
            FoodBlue.Add(food.transform);
        }
    }

    IEnumerator RotateSnake(float x = 0.0f, float y = 0.0f, float z = 0.0f)
    {
        isRotate = true;
        var duration = 0.1f;
        var startTime = Time.time;
        var startRotation = transform.rotation;
        var endRotation = transform.rotation * Quaternion.Euler(x, y, z);

        while (true)
        {
            var k = (Time.time - startTime) / duration;

            if (k >= 1) break;

            transform.rotation = Quaternion.Slerp(startRotation, endRotation, k);
            yield return null;
        }
        transform.rotation = endRotation;
        isRotate = false;
    }

    IEnumerator MoveSnake()
    {
        while (true)
        {
            if (isRotate == false)
            {
                AddScore(1);
                CheckBarrier();
                EatBone();

                if (isBarrierForward == false)
                {
                    _bone.transform.position = transform.position;
                    _bone.transform.rotation = transform.rotation;

                    foreach (var bone in Tails)
                    {
                        var tempP = bone.position;
                        var tempR = bone.rotation;
                        bone.position = _bone.transform.position;
                        bone.rotation = _bone.transform.rotation;
                        _bone.transform.position = tempP;
                        _bone.transform.rotation = tempR;
                    }

                    if (isRotate == false)
                    {
                        transform.Translate(Vector3.forward);
                    }
                }
                else
                {
                    if (isBarrierUp == true)
                    {
                        StartCoroutine(RotateSnake(x: 90));
                    }
                    else if (isBarrierDown == true)
                    {
                        StartCoroutine(RotateSnake(x: -90));
                    }
                    else
                    {
                        StartCoroutine(RotateSnake(x: -90));
                    }
                }
            }
            yield return new WaitForSeconds(moveSpeed);
        }
    }

    IEnumerator JumpSnake()
    {
        if (isJumpSnake == false && _jump > 0)
        {
            isJumpSnake = true;

            var tempMovespeed = moveSpeed;
            moveSpeed = jumpSpeed;
            yield return new WaitForSeconds(jumpTime);

            moveSpeed = tempMovespeed;
            isJumpSnake = false;
            _jump--;
        }
        else
        {
            yield return null;
        }
    }

    IEnumerator TimeGame()
    {
        while (true)
        {
            if (_timeToLost > 0)
            {
                _timeToLost--;
            }
            else
            {
                SceneManager.LoadScene(0);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void FlySnake()
    {
        if (isRotate == false)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(RotateSnake(z: 90f));
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(RotateSnake(z: -90f));
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine(RotateSnake(y: -90f));
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine(RotateSnake(y: 90f));
            }
            if (isBarrierUp == false)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    StartCoroutine(RotateSnake(x: -90f));
                }
            }

            if (isBarrierDown == false)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    StartCoroutine(RotateSnake(x: 90f));
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(JumpSnake());
            }

        }
    }

    private void EatBone()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))
        {
            if (hit.collider.tag == "Bone")
            {
                DeteleTail();
            }
        }
    }

    private void CheckBarrier()
    {
        RaycastHit hitForward;
        RaycastHit hitUp;
        RaycastHit hitDown;

        var upZ = new Vector3(0, 0, 1.2f);
        var upRay = transform.forward * 0.01f + (upZ.z * transform.up);

        var downZ = new Vector3(0, 0, -1.2f);
        var dowmRay = transform.forward * 0.01f + (downZ.z * transform.up);

        //Проверка лучей
        //Debug.DrawRay(transform.position, upRay, Color.green, 1f, false);
        //Debug.DrawRay(transform.position, dowmRay, Color.yellow, 1f, false);
        //Debug.DrawRay(transform.position, transform.forward, Color.blue, 1f, false);

        if (isRotate == false)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hitForward, 1f))
            {
                if (hitForward.collider.tag == "Barrier")
                {
                    isBarrierForward = true;
                }
            }
            else
            {
                isBarrierForward = false;
            }

            if (Physics.Raycast(transform.position, upRay, out hitUp, 1f))
            {
                if (hitUp.collider.tag == "Barrier")
                {
                    isBarrierUp = true;
                }
            }
            else
            {
                isBarrierUp = false;
            }

            if (Physics.Raycast(transform.position, dowmRay, out hitDown, 1f))
            {
                if (hitDown.collider.tag == "Barrier")
                {
                    isBarrierDown = true;
                }
            }
            else
            {
                isBarrierDown = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            if (collision.gameObject.tag == tags[i])
            {
                AddTail(collision, i);
                return;
            }
        }
    }

    private void AddTail(Collision collision, int count)
    {
        Destroy(collision.gameObject);

        var bone = Instantiate(BonePrefab);
        bone.transform.position = _bone.transform.position;
        bone.transform.rotation = _bone.transform.rotation;
        bone.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        Tails.Add(bone.transform);
        AddMoveSpeed();
        AddScore(scorePoint[count]);
        AddTime(timePoint[count]);
        if (count == 2)
        {
            AddJump();
        }
    }

    private void AddJump()
    {
        _jump++;
    }

    private void AddScore(int score = 10)
    {
        _score += score;
    }

    private void AddTime(int time = 10)
    {
        _timeToLost += time;
    }

    private void AddMoveSpeed(float speed = 0.01f)
    {
        if (moveSpeed - speed >= maxMoveSpeed)
        {
            moveSpeed -= speed;
        }
    }

    private void DeteleTail()
    {
        if (Tails.Count >= 1)
        {
            Destroy(Tails[Tails.Count - 1].gameObject);
            Tails.RemoveAt(Tails.Count - 1);
            transform.position = new Vector3(0, 0, 2);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            for (int j = 0; j <= Tails.Count - 1; j++)
            {
                Tails[j].gameObject.transform.position = new Vector3(0f, 0f, 0f);
            }
            _life -= 1;
        }
        else
        {
            _life -= 1;
        }
        if (_life == 0)
        {
            SceneManager.LoadScene(0);
        }
    }

}

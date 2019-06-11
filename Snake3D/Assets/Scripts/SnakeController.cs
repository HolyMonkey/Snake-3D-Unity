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
	[Range(1.01f, 1.1f)]
	public float Acceleration;
    [Range(4, 8)]
    public float rotationSpeed;
	[Range(0, 6)]
	public float AmplitudeMaximum;
	public bool isAmplitudeMove = false;
	public GameObject Bone;
    public UnityEvent OnEat;
    private Transform _transform;
    private Ghost _isGhost;
	private float _amplitude = 3f;
	private float _oneFifteenth;
	private bool _direction = false;

	private void Start()
    {
		_oneFifteenth = AmplitudeMaximum / 15f;
		_transform = GetComponent<Transform>();
        _isGhost = GetComponent<Ghost>();
    }

	private void changeAmplitude()
	{
		_amplitude += _direction ? _oneFifteenth : _oneFifteenth * -1f;
		if(_amplitude >= AmplitudeMaximum) {
			_direction = false;
		}
		if(_amplitude <= (AmplitudeMaximum * -1)) {
			_direction = true;
		}
	}

	private void MoveAmplitude() {
		_transform.Translate(new Vector3(_amplitude, 0, 0) * Time.deltaTime * Tails.Count / 2);
		changeAmplitude();
	}

	private void Update()
    {
        MoveSnake(_transform.position + _transform.forward * Speed);
		float angle = Input.GetAxis("Horizontal") * rotationSpeed;
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

		if(isAmplitudeMove) {
			MoveAmplitude();
		}
	}

	protected void OnHitBarrier()
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

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Barrier" && _isGhost.IsGhost())
        {
            OnHitBarrier();
        }

        if (collision.gameObject.tag == "Border" && _isGhost.IsGhost())
        {
            SceneManager.LoadScene(0);
        }

        if (collision.gameObject.tag == "Food")
        {
            Destroy(collision.gameObject);
            var bone = Instantiate(BonePrefab);
            Tails.Add(bone.transform);
            Speed *= Acceleration;
            if (OnEat != null)
            {
                OnEat.Invoke();
            }
        }             
    }
}

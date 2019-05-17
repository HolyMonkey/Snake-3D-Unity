using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float speedMove = 1.2f;
    public float speedRotate = 8f;

    public LayerMask maskObstacles;

    private Vector3 _position;

    void Start()
    {
        _position = target.InverseTransformPoint(transform.position);
    }
  
    void Update()
    {
        var currentPosition = target.TransformPoint(_position);
        transform.position = Vector3.Lerp(transform.position,currentPosition,speedMove * Time.deltaTime);
        var cuttentRotation = target.rotation;
        cuttentRotation *= Quaternion.Euler(20f,0,0);
        transform.rotation = Quaternion.Lerp(transform.rotation, cuttentRotation, speedRotate * Time.deltaTime);

        RaycastHit hit;
        if (Physics.Raycast(target.position, transform.position - target.position, out hit, Vector3.Distance(transform.position, target.position), maskObstacles))
        {
            transform.position = hit.point;
            transform.LookAt(target);
        }


    }
}

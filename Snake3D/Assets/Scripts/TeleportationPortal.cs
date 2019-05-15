using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationPortal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Transform _collisionTransform = collision.transform;
            if (_collisionTransform.GetComponent<SnakeController>().timeToTeleport < 0)
            {
                Vector3 _position = new Vector3(_collisionTransform.position.x < 0 ? 18.98f : -18.98f, _collisionTransform.position.y, _collisionTransform.position.z);
                _collisionTransform.localPosition = _position;
                _collisionTransform.GetComponent<SnakeController>().timeToTeleport = 1;
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SnakeControllerMatch3 : SnakeController
{
    protected override void OnCollisionEnter(Collision collision)
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
                Color32 objColor;
                objColor = collision.gameObject.GetComponent<MeshRenderer>().material.color;
                Destroy(collision.gameObject);
                var bone = Instantiate(BonePrefab);
                bone.GetComponent<Renderer>().material.color = objColor;
                Tails.Add(bone.transform);
                Speed *= 1.1f;
                if (OnEat != null)
                {
                    OnEat.Invoke();
                }
                if (Tails.Count >= 3)
                {
                    RemoveBone();
                }
        }             
    }

    private void RemoveBone()
    {
        for (int j = 0; j < Tails.Count - 2; j++)
        {
            var currentTailColor = Tails[j].GetComponent<Renderer>().material.color;
            if (Equals(currentTailColor, Tails[j + 1].GetComponent<Renderer>().material.color) &&
                Equals(currentTailColor, Tails[j + 2].GetComponent<Renderer>().material.color))
            {
                Destroy(Tails[j + 2].gameObject);
                Destroy(Tails[j + 1].gameObject);
                Destroy(Tails[j].gameObject);
                Tails.RemoveAt(j + 2);
                Tails.RemoveAt(j + 1);
                Tails.RemoveAt(j);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFood : MonoBehaviour
{
    public GameObject Food;    
    public int CountFood = 0;
        
    private void Start()
    {        
        StartCoroutine(CreateFoodCoroutine());
    }
        
    private void Update()
    {
        
    }
    
    IEnumerator CreateFoodCoroutine()
    {        
        while (CountFood < 10)
        {
            yield return new WaitForSeconds(1.0f);
            Instantiate(Food, transform.position, Quaternion.identity);
            CountFood += 1;
        }
    }
}

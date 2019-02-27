using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    public static int countUnlockedLevel = 1;
    [SerializeField]
    Sprite unlockedIcon;

    [SerializeField]
    Sprite lockedIcon;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i < countUnlockedLevel)
            {
                transform.GetChild(i).GetComponent<Image>().sprite = unlockedIcon;
                transform.GetChild(i).GetComponent<Button>().interactable = true;
            }

            else
            {
                transform.GetChild(i).GetComponent<Image>().sprite = lockedIcon;
                transform.GetChild(i).GetComponent<Button>().interactable = false;
            }
        }
    }
    
}

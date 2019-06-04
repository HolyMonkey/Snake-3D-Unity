using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void OnExitHandler()
    {
        Application.Quit(); 
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (SceneManager.GetActiveScene().name == (SelectLevel.countUnlockedLevel).ToString())
            {
                SelectLevel.countUnlockedLevel++;
            }
            SceneManager.LoadScene(0);
        }
    }
}

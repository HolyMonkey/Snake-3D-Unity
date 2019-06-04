using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnButtonLoadScene : MonoBehaviour
{
    [SerializeField]
    private ListLevels ListScenes;
    [SerializeField]
    private GameObject PrefabButton;

    private void Awake()
    {
        foreach (SceneInformation scene in ListScenes.Scenes)
        {
            if (scene.IdScene >0 && scene.IdScene < SceneManager.sceneCountInBuildSettings)
            {
                GameObject buttonClone = Instantiate(PrefabButton, transform);
                buttonClone.GetComponent<SceneLoadButton>().SetScene(scene);
            }
        }
    }
}

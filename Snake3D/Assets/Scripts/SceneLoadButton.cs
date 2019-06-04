using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadButton : MonoBehaviour
{
    private int _idSceneLoad;
    public Image ImageScene;
    public Text ButtonText;

    public void OnPlayhandler()
    {
        SceneManager.LoadScene(_idSceneLoad);
    }

    public void SetScene(SceneInformation scene)
    {
        ImageScene.sprite = scene.SceneImage;
        _idSceneLoad = scene.IdScene;
        ButtonText.text = scene.NameScene;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadButton : MonoBehaviour
{
    private SceneInformation _informationScene;
    private int _idSceneLoad;
    public Image ImageScene;
    public Text ButtonText;

    public void SetScene(SceneInformation scene)
    {
        _informationScene = scene;
    }
    
    public void OnPlayhandler()
    {
        SceneManager.LoadScene(_idSceneLoad);
    }

    public void SetImageAndText(SceneInformation scene)
    {
        _informationScene = scene;
        ImageScene.sprite = _informationScene.SceneImage;
        _idSceneLoad = _informationScene.IdScene;
        ButtonText.text = _informationScene.NameScene;
    }
}

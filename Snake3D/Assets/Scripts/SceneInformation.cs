using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Menu/New Scene")]
public class SceneInformation : ScriptableObject
{
    [SerializeField]
    private string _nameScene;
    [SerializeField]
    private Sprite _sceneImage;

    public string NameScene
    {
        get
        {
            return _nameScene;
        }
    }

    public int IdScene
    {
        get
        {
            return Int32.Parse(name);
        }
    }
    public Sprite SceneImage
    {
        get
        {
            return _sceneImage;
        }
    }
}

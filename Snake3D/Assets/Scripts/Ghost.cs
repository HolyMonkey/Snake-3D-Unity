using UnityEngine;

public class Ghost : MonoBehaviour
{
    private float _time;

    public void Make(float duration)
    {
        _time = duration;
    }

    public bool IsGhost()
    {
        return _time>0;
    }

    void Update()
    {
        if (_time >= 0)
            _time -= Time.deltaTime;
    }
}

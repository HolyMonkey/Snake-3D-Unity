using UnityEngine;

public class TeleportationDelay : MonoBehaviour
{
    private float _teleportDelay;

    public void TeleportDelay(float delay)
    {
        _teleportDelay = delay;
    }

    public float TeleportDelay()
    {
        return _teleportDelay;
    }

    void Update()
    {
        if (_teleportDelay >= 0) _teleportDelay -= Time.deltaTime;
    }
}

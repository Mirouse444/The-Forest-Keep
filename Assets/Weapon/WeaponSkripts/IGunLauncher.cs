using UnityEngine;

public interface IGunLauncher
{
    Transform transform { get; }
    void Fire(Vector2 direction, float powerMultiplier = 1f);
}
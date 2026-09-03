using UnityEngine;

public interface IWeaponLauncher
{
    Transform transform { get; }
    void Fire(Vector2 direction, float powerMultiplier = 1f);
}
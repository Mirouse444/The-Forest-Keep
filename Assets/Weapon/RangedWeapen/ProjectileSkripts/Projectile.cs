using System;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trail;

    private Action<Projectile> _returnAction;
    private bool _isDespawned;

    public event Action<LaunchData> LaunchAction;

    public void SetReleaseAction(Action<Projectile> returnAction)
    {
        _returnAction = returnAction;
    }

    public void Launch(LaunchData data, Vector3 position, Quaternion rotation)
    {
        _isDespawned = false;

        transform.SetPositionAndRotation(position, rotation);
        LaunchAction?.Invoke(data);
    }

    public void Despawn()
    {
        if (_isDespawned) return;
        _isDespawned = true;

        _trail.Clear();
        _returnAction?.Invoke(this);
    }
}
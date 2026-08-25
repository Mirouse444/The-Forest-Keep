using UnityEngine;
using System;

[RequireComponent(typeof(HealthComponent))]
public class EnemyCore : MonoBehaviour, ITarget
{
    private Action<EnemyCore> _returnToPool;
    private HealthComponent _health;

    public event Action<EnemyCore> OnDied;

    public Vector3 Position => transform.position;

    private void Awake() => _health = GetComponent<HealthComponent>();

    public void SetReleaseAction(Action<EnemyCore> returnAction) => _returnToPool = returnAction;

    private void OnEnable() => _health.State.OnDeath += Die;

    private void OnDisable() => _health.State.OnDeath -= Die;

    private void Die()
    {
        OnDied?.Invoke(this);
        _returnToPool?.Invoke(this);
    }
}
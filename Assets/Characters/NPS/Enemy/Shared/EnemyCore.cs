using UnityEngine;
using System;

[RequireComponent(typeof(HealthComponent))]
public class EnemyCore : MonoBehaviour, ITarget
{
    [SerializeField] private NightTrigger _trigger;

    private Action<EnemyCore> _returnToPool;
    private HealthComponent _health;

    public event Action<Vector3> OnDied;

    public Vector3 Position => transform.position;

    private void Awake()
    {
        _health = GetComponent<HealthComponent>();
    }

    public void SetReleaseAction(Action<EnemyCore> returnAction)
    {
        _returnToPool = returnAction;
    }

    private void OnEnable()
    {
        _health.State.OnDeath += Die;
    }

    private void OnDisable()
    {
        _health.State.OnDeath -= Die;
    }

    private void Die()
    {
        OnDied?.Invoke(transform.position);
        _trigger.OnEnemyDie();
        _returnToPool?.Invoke(this);
    }
}

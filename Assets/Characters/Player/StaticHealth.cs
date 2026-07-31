using UnityEngine;

[RequireComponent(typeof(HealthComponent))]

public class StaticHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private HealthComponent _health;

    private void Awake()
    {
        _health = GetComponent<HealthComponent>();
        _health.InitializeHealth(_maxHealth);
        _health.State.OnDeath += () => Debug.Log("Умер");
    }
}
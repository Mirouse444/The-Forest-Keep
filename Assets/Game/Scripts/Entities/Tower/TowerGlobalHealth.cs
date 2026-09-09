using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class TowerGlobalHealth : MonoBehaviour
{
    [SerializeField] private HealthComponent _health;
    [SerializeField, Min(0)] private int _maxHealth;

    private void Awake() => _health.InitializeHealth(_maxHealth);
}
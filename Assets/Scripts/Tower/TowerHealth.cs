using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class TowerHealth : MonoBehaviour
{
    [SerializeField] private int _MaxHealth;

    private HealthComponent _health;

    private void Awake()
    {
        _health = GetComponent<HealthComponent>();
        _health.InitializeHealth(_MaxHealth);
    }
    
    private void UpdateTower() => _MaxHealth = (int)(_MaxHealth * 1.35f + 0.5f);
}


using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class TowerGlobalHealth : MonoBehaviour
{
    [SerializeField] private HealthComponent _health;
    [SerializeField] private int _MaxHealth;

    private void Awake() => _health.InitializeHealth(_MaxHealth);
    
    private void UpdateTower() => _MaxHealth = (int)(_MaxHealth * 1.35f + 0.5f);
}
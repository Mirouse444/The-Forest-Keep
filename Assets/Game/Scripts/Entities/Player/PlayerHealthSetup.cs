using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class PlayerHealthSetup : MonoBehaviour
{
    [SerializeField, Min(0)] private int _fixedHealth = 100;

    private HealthComponent _healthComponent;
    
    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _healthComponent.InitializeHealth(_fixedHealth);
    }
}
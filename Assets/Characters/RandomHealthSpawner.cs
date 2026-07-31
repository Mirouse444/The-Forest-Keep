using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
internal class RandomHealthSpawner : MonoBehaviour
{
    [SerializeField] private int _minHealth = 20;
    [SerializeField] private int _maxHealth = 50;

    private HealthComponent _healthComponent;

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
    }

    private void OnEnable()
    {
        int randomHp = Random.Range(_minHealth, _maxHealth + 1);
        _healthComponent.InitializeHealth(randomHp);
    }
}

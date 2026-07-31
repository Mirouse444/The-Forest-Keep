using System;

[Serializable]
internal class HealthModel : IHealthState
{
    private int _maxHealth;
    private int _currentHealth;

    public event Action OnDeath;
    public event Action<int, int> OnHealthChanged;

    public void Initialization(int newMaxHealth)
    {
        _maxHealth = newMaxHealth;
        _currentHealth = newMaxHealth;
    }

    public void ApplyDamage(int finalDamage)
    {
        if (_currentHealth <= 0 || finalDamage <= 0) return;

        _currentHealth -= finalDamage;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            OnDeath?.Invoke();
        }

        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }
}

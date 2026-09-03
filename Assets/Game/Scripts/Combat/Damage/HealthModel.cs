using System;

[Serializable]
public class HealthModel : IHealthState
{
    private int _maxHealth;
    private int _currentHealth;

    public event Action OnDeath;
    public event Action OnApplyDamage;
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
        OnApplyDamage?.Invoke();
    }

    public bool AddHP(int health)
    {
        if(_currentHealth == _maxHealth) return false;
        
        _currentHealth = Math.Min(_currentHealth + health, _maxHealth);
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        return true;
    }

    public void ResetToMax()
    {
        _currentHealth = _maxHealth;
        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }
}
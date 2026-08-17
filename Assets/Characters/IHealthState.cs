using System;

public interface IHealthState
{
    event Action OnDeath;
    event Action OnApplyDamage;
    event Action<int, int> OnHealthChanged;
}
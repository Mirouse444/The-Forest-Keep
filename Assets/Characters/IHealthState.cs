using System;

public interface IHealthState
{
    event Action OnDeath;
    event Action<int, int> OnHealthChanged;
}
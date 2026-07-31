using System;

public interface IDayNightCycle
{
    event Action<int> OnDayStarted;
    event Action<int> OnNightStarted;
}

IEnemyFactory
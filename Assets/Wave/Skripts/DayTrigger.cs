using UnityEngine;
using System;

public class DayTrigger : MonoBehaviour
{
    private int _currentEnemyCount = 0;
    
    public event Action OnDayStarted;

    public void AddEnemyCount(NightConfig config)
    {
        foreach (var wave in config.Waves)
            _currentEnemyCount += wave.Count;
    }

    public void AddEnemy(EnemyCore enemy)
    {
        enemy.OnDied += OnEnemyDie;
    }

    private void OnEnemyDie(EnemyCore enemy)
    {
        enemy.OnDied -= OnEnemyDie;
        _currentEnemyCount--;
        
        if(_currentEnemyCount <= 0)
            OnDayStarted?.Invoke();
    }
}
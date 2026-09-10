using UnityEngine;
using UnityEngine.Events;

public class DayTrigger : MonoBehaviour
{
    [SerializeField] private WaveDirector _director;
    
    private int _currentEnemyCount;
    
    public UnityEvent OnDayStarted;

    private void OnEnable()
    {
        _director.OnWaveSpawn += AddEnemyCount;
        _director.OnEnemySpawn += AddEnemy;
    }

    private void AddEnemyCount(NightConfig config)
    {
        foreach (var wave in config.Waves)
            _currentEnemyCount += wave.Count;
    }

    private void AddEnemy(EnemyCore enemy) => enemy.OnDied += OnEnemyDie;

    private void OnEnemyDie(EnemyCore enemy)
    {
        enemy.OnDied -= OnEnemyDie;
        _currentEnemyCount--;
        
        if(_currentEnemyCount <= 0)
            OnDayStarted?.Invoke();
    }
}
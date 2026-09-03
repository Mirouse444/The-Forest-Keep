using System;
using UnityEngine;

public class SpawnerWavesConfig : MonoBehaviour
{
    [SerializeField] private NightConfig[] _nightConfigArray;
    [SerializeField] private NightTrigger _trigger;

    private int _currentNight = 0;
    
    public event Action<NightConfig> OnEnemyGo;

    private void OnEnable() => _trigger.OnNightStarted += EnemyGo;
    private void OnDisable() => _trigger.OnNightStarted -= EnemyGo;

    private void EnemyGo()
    {
        if (_currentNight < _nightConfigArray.Length)
            OnEnemyGo?.Invoke(_nightConfigArray[_currentNight]);
        _currentNight++;
    }
}
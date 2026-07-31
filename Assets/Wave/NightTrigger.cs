using System;
using System.Collections.Generic;
using UnityEngine;
using static NightConfig;

public class NightTrigger : MonoBehaviour
{
    private int _currentEnemyCount = 0;

    public event Action OnNightStarted;


    public void SetCurrentEnemyCount(IReadOnlyList<WaveGroup> enemyList)
    {
        for (int i = 0; i < enemyList.Count; i++)
            _currentEnemyCount += enemyList[i].Count;
    }

    public void OnEnemyDie()
    {
        _currentEnemyCount--;
        
        if(_currentEnemyCount == 0)
            OnNightStarted.Invoke();
    }
}
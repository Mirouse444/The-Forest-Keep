using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IEnemyFactory
{
    [System.Serializable]
    private struct EnemyPoolMapping
    {
        [SerializeField] private EnemyCore _enemyPrefab;
        [SerializeField] private EnemyPool _enemyPool;

        public EnemyCore Prefab  => _enemyPrefab ;
        public EnemyPool Pool => _enemyPool;
    }
    
    [SerializeField] private EnemyPoolMapping[]  _poolsConfiguration;

    private Dictionary<EnemyCore, EnemyPool> _enemyDictionary;

    private void Awake()
    {
        _enemyDictionary = new Dictionary<EnemyCore, EnemyPool>(_poolsConfiguration.Length);

        foreach (var mapping  in _poolsConfiguration)
            if (!_enemyDictionary.TryAdd(mapping.Prefab , mapping.Pool))
                Debug.LogWarning($"[EnemyFactory] Пул для префаба {mapping.Prefab.name} уже добавлен!");
    }

    public EnemyCore GetEnemy(EnemyCore prefab)
    {
        if (_enemyDictionary.TryGetValue(prefab, out EnemyPool pool))
            return pool.GetEnemy;
        
        Debug.LogError($"[EnemyFactory] Пул для префаба {prefab.name} не найден в конфигурации!");
        return null;
    }
}
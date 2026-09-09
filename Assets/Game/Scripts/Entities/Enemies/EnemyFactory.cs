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

        foreach (var mapping in _poolsConfiguration)
            _enemyDictionary.Add(mapping.Prefab, mapping.Pool);
    }

    public EnemyCore GetEnemy(EnemyCore prefab)
    {
        return _enemyDictionary.TryGetValue(prefab, out EnemyPool pool) ? pool.GetEnemy : null;
    }
}
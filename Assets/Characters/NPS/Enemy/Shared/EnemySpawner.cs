using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

internal class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyCore _enemyPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _spawnInterval;

    private IObjectPool<EnemyCore> _enemyPool;
    private WaitForSeconds _spawnDelay;

    private void Awake()
    {
        _enemyPool = new ObjectPool<EnemyCore>(
            createFunc: CreateEnemy,
            actionOnGet: OnTakeEnemyFromPool,
            actionOnRelease: OnReturnEnemyToPool,
            actionOnDestroy: OnDestroyEnemy,
            collectionCheck: true,
            defaultCapacity: 15,
            maxSize: 30
        );

        _spawnDelay = new WaitForSeconds(_spawnInterval);
}

    private void Start()
    {
        StartCoroutine(StartSpawn());
    }

    private IEnumerator StartSpawn()
    {
        while (true)
        {
            yield return _spawnDelay;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        _enemyPool.Get();
    }

    private EnemyCore CreateEnemy()
    {
        EnemyCore enemy = Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity);
        enemy.SetReleaseAction(_enemyPool.Release);

        return enemy;
    }

    private void OnTakeEnemyFromPool(EnemyCore enemy)
    {
        enemy.transform.position = _spawnPoint.position;
        enemy.gameObject.SetActive(true);
    }


    private void OnReturnEnemyToPool(EnemyCore enemy)
    {
        enemy.gameObject.SetActive(false);
    }
    private void OnDestroyEnemy(EnemyCore enemy)
    {
        Destroy(enemy.gameObject);
    }
}
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private EnemyCore _enemyPrefab;

    private IObjectPool<EnemyCore> _enemyPool;
    
    private void Awake() =>
        _enemyPool = new ObjectPool<EnemyCore>(
            createFunc: CreateEnemy,
            actionOnGet: OnTakeEnemyFromPool,
            actionOnRelease: OnReturnEnemyToPool,
            actionOnDestroy: OnDestroyEnemy,
            collectionCheck: true,
            defaultCapacity: 15,
            maxSize: 30
        );

    public EnemyCore GetEnemy => _enemyPool.Get();

    private EnemyCore CreateEnemy()
    {
        EnemyCore enemy = Instantiate(_enemyPrefab);
        enemy.SetReleaseAction(_enemyPool.Release);

        return enemy;
    }

    private void OnTakeEnemyFromPool(EnemyCore enemy) => enemy.gameObject.SetActive(true);
    private void OnReturnEnemyToPool(EnemyCore enemy) => enemy.gameObject.SetActive(false);
    private void OnDestroyEnemy(EnemyCore enemy) => Destroy(enemy.gameObject);
}
using UnityEngine;
using UnityEngine.Pool;

internal class StuckProjectilePool : MonoBehaviour
{
    [SerializeField] private StuckProjectile _arrowPrefab;

    private IObjectPool<StuckProjectile> _arrowPool;

    private void Awake()
    {
        _arrowPool = new ObjectPool<StuckProjectile>(
            createFunc: CreateStuckArrow,
            actionOnGet: OnTakeStuckArrowFromPool,
            actionOnRelease: OnReturnStuckArrowToPool,
            actionOnDestroy: OnDestroyStuckArrowPool,
            collectionCheck: true,
            defaultCapacity: 15,
            maxSize: 100
        );
    }

    public StuckProjectile GetStuckArrow()
    {
        return _arrowPool.Get();
    }

    private StuckProjectile CreateStuckArrow()
    {
        StuckProjectile arrow = Instantiate(_arrowPrefab);
        arrow.SetReleaseAction(_arrowPool.Release);

        return arrow;
    }

    private void OnTakeStuckArrowFromPool(StuckProjectile stuckArrow)
    {
        stuckArrow.gameObject.SetActive(true);
    }


    private void OnReturnStuckArrowToPool(StuckProjectile stuckArrow)
    {
        stuckArrow.gameObject.SetActive(false);
    }
    private void OnDestroyStuckArrowPool(StuckProjectile stuckArrowPool)
    {
        Destroy(stuckArrowPool.gameObject);
    }
}

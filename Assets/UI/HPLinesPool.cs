using System;
using UnityEngine;
using UnityEngine.Pool;

public class HPLinesPool : MonoBehaviour, IHPLineSpawner
{
    [SerializeField] private HPLineSO _lineSo;
    [SerializeField] private HPLine _hpLinePrefab;

    private ObjectPool<HPLine> _pool;

    private void Awake()
    {
        _lineSo.HPLineSpawner = this;
        _pool = new ObjectPool<HPLine>
        (
            createFunc: CreateLine,
            actionOnGet: OnTakeLineFromPool,
            actionOnRelease: OnReturnLineToPool,
            actionOnDestroy: OnDestroyLine,
            collectionCheck: false,
            defaultCapacity: 14,
            maxSize: 30
        );
    }

    public HPLine GetHPLine(out Action<HPLine> releaseAction)
    {
        releaseAction = _pool.Release;
        return _pool.Get();
    }

    private HPLine CreateLine() => Instantiate(_hpLinePrefab, transform);
    private void OnTakeLineFromPool(HPLine hpLine) => hpLine.gameObject.SetActive(true);
    private void OnReturnLineToPool(HPLine hpLine) => hpLine.gameObject.SetActive(false);
    private void OnDestroyLine(HPLine hpLine) => Destroy(hpLine.gameObject);
}
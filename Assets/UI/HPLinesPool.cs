using System;
using UnityEngine;
using UnityEngine.Pool;

public class HPLinesPool : MonoBehaviour
{
    [SerializeField] private HPLine _hpLinePrefab;

    private ObjectPool<HPLine> _pool;

    public Action<HPLine> ReleaseToPool { get; private set; }

    private void Awake()
    {
        InitializePool();
    }
    private void InitializePool()
    {
        if (_pool != null) return;

        _pool = new ObjectPool<HPLine>(
              createFunc: CreateLine,
              actionOnGet: OnTakeLineFromPool,
              actionOnRelease: OnReturnLineToPool,
              actionOnDestroy: OnDestroyLine,
              collectionCheck: false,
              defaultCapacity: 14,
              maxSize: 30
        );

        ReleaseToPool = _pool.Release;
    }

    public HPLine GetHPLine()
    {
        InitializePool();
        return _pool.Get();
    }
    private HPLine CreateLine() => Instantiate(_hpLinePrefab, transform);
    private void OnTakeLineFromPool(HPLine hpLine) => hpLine.gameObject.SetActive(true);
    private void OnReturnLineToPool(HPLine hpLine) => hpLine.gameObject.SetActive(false);
    private void OnDestroyLine(HPLine hpLine) => Destroy(hpLine.gameObject);
}
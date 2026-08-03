using System;
using UnityEngine;

internal class StuckProjectile : MonoBehaviour
{
    [SerializeField] private float _returnTime;

    private float _currentTime;
    private Action<StuckProjectile> _returnToPool;
    private bool _isReleased;
    private Vector3 _defaultScale;

    private void Awake()
    {
        _defaultScale = transform.localScale;
    }

    public void SetReleaseAction(Action<StuckProjectile> returnAction)
    {
        _returnToPool = returnAction;
    }

    public void Init(Transform pointOfImpact, Transform carrier)
    {
        if (!carrier.gameObject.activeInHierarchy)
        {
            ReturnToPool();
            return;
        }

        _isReleased = false;
        _currentTime = 0f;

        transform.SetPositionAndRotation(pointOfImpact.position,pointOfImpact.rotation);

        transform.SetParent(carrier);
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _returnTime)
            ReturnToPool();
    }

    private void OnDisable()
    {
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (_isReleased) return;

        _isReleased = true;
        _returnToPool?.Invoke(this);
    }
}
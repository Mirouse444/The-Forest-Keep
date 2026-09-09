using System;
using UnityEngine;

public class StuckProjectile : MonoBehaviour
{
    
    [SerializeField] private float _returnTime;

    private float _currentTime;
    private Action<StuckProjectile> _returnToPool;
    private bool _isReleased;
    
    private Transform _carrier;
    private Vector3 _localPositionOffset;
    private Quaternion _localRotationOffset;
    
    public void SetReleaseAction(Action<StuckProjectile> returnAction) => _returnToPool = returnAction;

    public void Launch(Transform pointOfImpact, Transform carrier)
    {
        if (!carrier.gameObject.activeInHierarchy)
        {
            ReturnToPool();
            return;
        }

        _isReleased = false;
        _currentTime = 0f;
        _carrier = carrier;
        
        transform.SetPositionAndRotation(pointOfImpact.position, pointOfImpact.rotation);
        
        _localPositionOffset = _carrier.InverseTransformPoint(transform.position);
        _localRotationOffset = Quaternion.Inverse(_carrier.rotation) * transform.rotation;
    }

    private void LateUpdate()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _returnTime)
        {
            ReturnToPool();
            return;
        }
        
        if (_carrier && _carrier.gameObject.activeInHierarchy)
        {
            transform.position = _carrier.TransformPoint(_localPositionOffset);
            transform.rotation = _carrier.rotation * _localRotationOffset;
        }
        else
        {
            ReturnToPool();
        }
    }

    private void OnDisable() => ReturnToPool();

    private void ReturnToPool()
    {
        if (_isReleased) return;

        _isReleased = true;
        _carrier = null; 
        _returnToPool?.Invoke(this);
    }
}
using UnityEngine;

public class CannonRotator : MonoBehaviour
{
    [SerializeField] private Transform _barrelPivot;
    
    [Header("Rotation Limits")]
    [SerializeField] private float _minAngle = -45f;
    [SerializeField] private float _maxAngle = 30f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private bool _invertAngle = true;
    
    private Transform _baseTransform;
    public Transform BarrelPivot => _barrelPivot;

    private void Awake()
    {
        _baseTransform = _barrelPivot.parent != null ? _barrelPivot.parent : transform;
    }

    public bool AimAt(Vector2 globalDirection)
    {
        Vector3 localDir = _baseTransform.InverseTransformDirection(globalDirection);
        float localAngle = Mathf.Atan2(localDir.y, localDir.x) * Mathf.Rad2Deg;

        if (_invertAngle) 
            localAngle = -localAngle;

        float clampedAngle = Mathf.Clamp(localAngle, _minAngle, _maxAngle);
        Quaternion targetRotation = Quaternion.Euler(0, 0, clampedAngle);
        
        _barrelPivot.localRotation = Quaternion.Slerp(_barrelPivot.localRotation, targetRotation, Time.deltaTime * _rotationSpeed);
        
        if (Mathf.Abs(localAngle - clampedAngle) > 1f)
            return false; 

        return Quaternion.Angle(_barrelPivot.localRotation, targetRotation) < 3f;
    }
}
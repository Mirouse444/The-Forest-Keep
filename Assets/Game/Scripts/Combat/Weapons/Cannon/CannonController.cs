using UnityEngine;

[RequireComponent(typeof(CannonRotator), typeof(CannonWeapon))]
public class CannonController : MonoBehaviour
{
    [SerializeField] private CannonStats[] _stats;
    [SerializeField] private float _gravityScale = 1f;

    private CannonRotator _rotator;
    private CannonWeapon _weapon;
    private int _currentLevel = 0;
    
    private Vector2 _calculatedDirection;

    private CannonStats CurrentStats => _stats[_currentLevel];

    private void Awake()
    {
        _rotator = GetComponent<CannonRotator>();
        _weapon = GetComponent<CannonWeapon>();
    }

    public bool AimAt(Vector3 targetPosition)
    {
        float gravity = Mathf.Abs(Physics2D.gravity.y * _gravityScale);
        
        Vector3 startPos = _rotator.BarrelPivot.position; 

        if (!BallisticMath.CalculateLowArc(startPos, targetPosition, CurrentStats.ProjectileSpeed, gravity, out Vector2 requiredDir))
            return false; 

        _calculatedDirection = requiredDir;
        return _rotator.AimAt(requiredDir);
    }

    public bool TryFire()
    {
        if (!_weapon.CanFire) return false;

        _weapon.Fire(_calculatedDirection, CurrentStats);
        return true;
    }
    
    public void LevelUp()
    {
        if (_currentLevel < _stats.Length - 1)
            _currentLevel++;
    }
}
using UnityEngine;

[RequireComponent(typeof(CannonRotator), typeof(IWeaponLauncher))]
public class CannonController : MonoBehaviour
{
    [SerializeField] private CannonInfo _info;
    [SerializeField] private float _gravityScale = 1f;
    [SerializeField, Min(0)] private float _fireRate = 1.5f;

    private IWeaponLauncher _launcher;
    private CannonRotator _rotator;
    private Vector2 _requiredDir;
    private float _timer;

    private void Awake()
    {
        _rotator = GetComponent<CannonRotator>();
        _launcher = GetComponent<IWeaponLauncher>();
    }

    public bool AimAt(Vector3 targetPosition)
    {
        float gravity = Mathf.Abs(Physics2D.gravity.y * _gravityScale);
        Vector3 startPos = _rotator.BarrelPivot.position;

        return BallisticMath.CalculateLowArc(startPos, targetPosition, _info.ProjectileSpeed, gravity, out _requiredDir) && _rotator.AimAt(_requiredDir);
    }

    public void TryFire()
    {
        if (_timer + _fireRate <= Time.time)
        {
            _launcher.Fire(_requiredDir);
            _timer = Time.time;
        }
    }
}
using UnityEngine;

[RequireComponent(typeof(TargetScanner))]
public class SkeletonArcherBrain : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float _attackCooldown = 2f;
    [SerializeField] private Transform _shootPlace;
    [SerializeField] private ProjectileLauncher _launcher;

    [Header("Physics (Must match arrow Rigidbody2D)")]
    [SerializeField] private float _gravityScale = 1f;

    private TargetScanner _scanner;
    private float _lastAttackTime;

    private void Awake()
    {
        _scanner = GetComponent<TargetScanner>();
    }

    private void Update()
    {
        if (_scanner.CurrentTarget == null) return;

        if (Time.time - _lastAttackTime >= _attackCooldown)
            TryShootTarget(_scanner.TargetExactPosition);
    }

    private void TryShootTarget(Vector3 targetPosition)
    {
        float gravity = Mathf.Abs(Physics2D.gravity.y * _gravityScale);

        if (BallisticMath.CalculateLowArc(_shootPlace.position, targetPosition, _launcher.ShootSpeed, gravity, out Vector2 direction))
        {
            _lastAttackTime = Time.time;
            _launcher.Fire(direction);
        }
    }
}
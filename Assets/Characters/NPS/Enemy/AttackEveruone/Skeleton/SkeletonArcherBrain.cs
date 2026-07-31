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

        if (CalculateHighArc(_shootPlace.position, targetPosition, _launcher.ShootSpeed, gravity, out Vector2 direction))
        {
            _lastAttackTime = Time.time;
            _launcher.Fire(direction);
        }
    }

    private bool CalculateHighArc(Vector2 start, Vector2 target, float speed, float gravity, out Vector2 direction)
    {
        direction = Vector2.zero;
        Vector2 diff = target - start;
        float x = Mathf.Abs(diff.x);
        float y = diff.y;

        float s2 = speed * speed;
        float s4 = s2 * s2;

        float root = s4 - gravity * (gravity * x * x + 2 * y * s2);

        if (root < 0) return false;

        float angle = Mathf.Atan2(s2 + Mathf.Sqrt(root), gravity * x);

        float dirX = Mathf.Cos(angle) * Mathf.Sign(diff.x);
        float dirY = Mathf.Sin(angle);

        direction = new Vector2(dirX, dirY).normalized;
        return true;
    }
}
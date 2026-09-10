using UnityEngine;

[RequireComponent(typeof(TargetScanner))]
public class SkeletonArcherBrain : MonoBehaviour
{
    private enum State { Walk, Aiming, Idle }
    
    [Header("Attack Settings")]
    [SerializeField] private float _attackCooldown = 2f;
    [SerializeField] private float _aimDuration = 0.6f;
    [SerializeField] private float _directShootDistance = 4f;
    [SerializeField] private Transform _shootPlace;
    [SerializeField] private WeaponInfoSo _info;
    [SerializeField] private ProjectileLauncher _launcher;

    [Header("Animation")]
    [SerializeField] private Animator _animator;

    [Header("Physics")]
    [SerializeField] private float _gravityScale = 1f;

    [Header("Setting")]
    [SerializeField] private TargetScanner _scanner;
    [SerializeField] private HealthComponent _healthComponent;

    private void OnEnable() => _healthComponent.State.OnApplyDamage += CancelAiming;
    private void OnDisable() => _healthComponent.State.OnApplyDamage -= CancelAiming;

    private State _currentState = State.Walk;
    private float _lastAttackTime;
    private float _aimTimer;
    private Vector2 _currentAimDirection;

    private void Update()
    {
        switch (_currentState)
        {
            case State.Walk:
                HandleWalkState();
                break;
            case State.Aiming:
                HandleAimingState();
                break;
            case State.Idle:
                HandleIdleState();
                break;
        }
    }

    private void HandleWalkState()
    {
        if (_scanner.CurrentTarget != null && Time.time - _lastAttackTime >= _attackCooldown)
            if (TryCalculateTrajectory(out _currentAimDirection))
                StartAiming();
    }

    private void StartAiming()
    {
        _currentState = State.Aiming;
        _aimTimer = 0f;
        _animator.SetTrigger("DrawBow");
    }

    private void HandleAimingState()
    {
        if (!TryCalculateTrajectory(out _currentAimDirection))
        {
            CancelAiming();
            return;
        }
        
        _aimTimer += Time.deltaTime;
        if (_aimTimer >= _aimDuration)
            Shoot();
    }

    private bool TryCalculateTrajectory(out Vector2 direction)
    {
        direction = Vector2.zero;
        if (_scanner.CurrentTarget == null) return false;
        
        Vector2 targetPos = (Vector2)_scanner.CurrentTarget.Position + Vector2.up * 1.5f;
        float distanceSqr = ((Vector2)_shootPlace.position - targetPos).sqrMagnitude;
        
        if (distanceSqr <= _directShootDistance * _directShootDistance)
        {
            direction = (targetPos - (Vector2)_shootPlace.position).normalized;
            return true;
        }

        float gravity = Mathf.Abs(Physics2D.gravity.y * _gravityScale);
        return BallisticMath.CalculateLowArc(_shootPlace.position, targetPos, _info.ShootSpeed, gravity, out direction);
    }

    private void HandleIdleState()
    {
        if (Time.time - _lastAttackTime >= _attackCooldown)
            _currentState = State.Walk;
    }
    
    private void Shoot()
    {
        _animator.SetTrigger("ReleaseBow");
        
        _lastAttackTime = Time.time;
        _currentState = State.Idle;
    }
    
    private void CancelAiming()
    {
        _animator.SetTrigger("CancelAim");
        _currentState = State.Walk;
    }
    
    private void AnimationEvent_SpawnArrow() => _launcher.Fire(_currentAimDirection);
}
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
    [SerializeField] private ProjectileLauncher _launcher;

    [Header("Animation & Rigging")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _aimBone; 
    [SerializeField] private float _aimBoneRotationOffset;
    [SerializeField] private float _aimLerpSpeed = 15f;

    [Header("Physics")]
    [SerializeField] private float _gravityScale = 1f;

    [Header("Setting")]
    [SerializeField] private TargetScanner _scanner;
    
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
        
        RotateAimBone(_currentAimDirection);
        
        _aimTimer += Time.deltaTime;
        if (_aimTimer >= _aimDuration)
            Shoot();
    }

    private bool TryCalculateTrajectory(out Vector2 direction)
    {
        direction = Vector2.zero;
        if (_scanner.CurrentTarget == null) return false;
        
        Vector3 targetPos = _scanner.TargetExactPosition;
        float distanceSqr = ((Vector2)(_shootPlace.position - targetPos)).sqrMagnitude;
        
        if (distanceSqr <= _directShootDistance * _directShootDistance)
        {
            direction = (targetPos - _shootPlace.position).normalized;
            return true;
        }

        float gravity = Mathf.Abs(Physics2D.gravity.y * _gravityScale);
        return BallisticMath.CalculateLowArc(_shootPlace.position, targetPos, _launcher.ShootSpeed, gravity, out direction);
    }

    private void HandleIdleState()
    {
        if (Time.time - _lastAttackTime >= _attackCooldown)
            _currentState = State.Walk;
    }
    
    private void RotateAimBone(Vector2 direction)
    {
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle + _aimBoneRotationOffset);
        _aimBone.rotation = Quaternion.Lerp(_aimBone.rotation, targetRotation, Time.deltaTime * _aimLerpSpeed);
    }

    private void Shoot()
    {
        _animator.SetTrigger("ReleaseBow");
        _launcher.Fire(_currentAimDirection);
        
        _lastAttackTime = Time.time;
        _currentState = State.Idle;
    }

    private void CancelAiming()
    {
        _animator.SetTrigger("CancelAim");
        _currentState = State.Walk;
    }
}
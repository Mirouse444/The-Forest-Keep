using UnityEngine;

internal class MobeMover : MonoBehaviour
{
    private const float _fault = 0.05f;
    
    [SerializeField] private float _defaultSpeed;
    [SerializeField] private float _sprintSpeed;

    [Header("Combat")]
    [SerializeField] private float _stopDistance;
    [SerializeField] private float _stopHurtTime;
    
    [Header("Settings")]
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private TargetScanner _scanner;
    [SerializeField] private Transform _mainTarget;
    [SerializeField] private Animator _animator;
    
    private float _timer;

    private void OnEnable() => _healthComponent.State.OnApplyDamage += Stun;
    private void OnDisable() => _healthComponent.State.OnApplyDamage -= Stun;

    private void Stun() => _timer = 0;

    private void Update()
    {
        if (_timer >= _stopHurtTime)
            Move();
        else
            _timer += Time.deltaTime;
    }
    
    private void Move()
    {
        Vector2 targetPosition = _mainTarget.position;
        
        float currentSpeed = _defaultSpeed;
        float animationSpeed = 1f;

        if (_scanner.CurrentTarget != null)
        {
            targetPosition = _scanner.TargetExactPosition;
            currentSpeed = _sprintSpeed;
            animationSpeed = 2f;
        }
        
        float distanceX = Mathf.Abs(targetPosition.x - transform.position.x);
        if (distanceX - _fault <= _stopDistance) 
        {
            _animator.SetFloat("MoveSpeed", 0f);
            return;
        }
        
        float maxStep = distanceX - _stopDistance;
        float defaultStep = currentSpeed * Time.deltaTime;
        float step = Mathf.Min(defaultStep, maxStep);
        
        int directionX = targetPosition.x >= transform.position.x ? 1 : -1;
        
        ApplyRotation(directionX);
        _animator.SetFloat("MoveSpeed", animationSpeed);
        transform.Translate(new Vector3(directionX * step, 0f, 0f), Space.World);
    }

    private void ApplyRotation(float directionX) => transform.rotation = Quaternion.Euler(0f, directionX < 0 ? 180f : 0f, 0f);
}
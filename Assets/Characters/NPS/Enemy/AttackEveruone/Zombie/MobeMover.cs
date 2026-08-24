using UnityEngine;

internal class MobeMover : MonoBehaviour
{
    [SerializeField] private float _defaultSpeed;
    [SerializeField] private float _sprintSpeed;

    [Header("Combat")]
    [SerializeField] private float _stopDistance;

    [SerializeField] private Animator _animator;
    
    [Header("Settings")]
    [SerializeField] private TargetScanner _scanner;
    [SerializeField] private Transform _mainTarget;

    private void Update() => Move();

    private void Move()
    {
        Vector2 targetPosition = _mainTarget.position;
        
        
        float currentSpeed = _defaultSpeed;

        if (_scanner.CurrentTarget != null)
        {
            targetPosition = _scanner.CurrentTarget.Position;
            currentSpeed = _sprintSpeed;
            _animator.SetBool("isMoving", true);
        }
        
        float distanceX = Mathf.Abs(targetPosition.x - transform.position.x);
        if (distanceX <= _stopDistance) return; 
        
        float maxStep = distanceX - _stopDistance;
        float defaultStep = currentSpeed * Time.deltaTime;
        float step = Mathf.Min(defaultStep, maxStep);
        
        int directionX = targetPosition.x >= transform.position.x ? 1 : -1;
        
        ApplyRotation(directionX);
        transform.Translate(new Vector3(directionX * step, 0f));
    }

    private void ApplyRotation(float directionX) => transform.rotation = Quaternion.Euler(0f, directionX < 0 ? 180f : 0f, 0f);
}
using UnityEngine;

internal class MobeMover : MonoBehaviour
{
    [SerializeField] private float _defaultSpeed;
    [SerializeField] private float _sprintSpeed;

    [Header("Combat")]
    [SerializeField] private float _stopDistance;

    private Vector3 _originalScale;
    private TargetScanner _scanner;
    

    private void Awake()
    {
        _scanner = GetComponent<TargetScanner>();
    }

    private void Start()
    {
        _originalScale = transform.localScale;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float currentDirectionX = 1; 
        float currentSpeed = _defaultSpeed;

        if (_scanner.CurrentTarget != null)
        {
            Vector3 targetPos = _scanner.CurrentTarget.Position;
            float distanceToTarget = Vector2.Distance(transform.position, targetPos);

            currentDirectionX = Mathf.Sign(targetPos.x - transform.position.x);
            ApplyRotation(currentDirectionX);

            if (distanceToTarget <= _stopDistance) return;

            currentSpeed = _sprintSpeed;
        }
        else
        {
            ApplyRotation(currentDirectionX);
        }

        transform.Translate(Vector3.right * currentDirectionX * Time.deltaTime * currentSpeed);
    }

    private void ApplyRotation(float directionX)
    {
        transform.localScale = new Vector3(
            Mathf.Abs(_originalScale.x) * directionX,
            _originalScale.y,
            _originalScale.z
        );
    }
}
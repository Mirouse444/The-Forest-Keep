using UnityEngine;

internal class TargetScanner : MonoBehaviour
{
    [Header("Vision Configuration")]
    [SerializeField, Min(0)] private float _visionRadius = 5f;
    [SerializeField, Min(0)] private float _loseTargetRadius = 7f;
    [SerializeField, Min(0)] private float _scanInterval = 0.25f;
    [SerializeField] private LayerMask _targetLayer;
    
    [Header("Blind Zone Settings")]
    [SerializeField, Min(0)] private float _blindZoneWidth = 0f;
    [SerializeField, Min(0)] private float _blindZoneHeight = 0f;
    [SerializeField] private Vector2 _blindZoneOffset = new Vector2(0f, 0f);

    private float _scanTimer;
    private Collider2D _targetCollider;
    private ContactFilter2D _contactFilter;
    private readonly Collider2D[] _visionResults = new Collider2D[10];
    
    public ITarget CurrentTarget { get; private set; }

    public Vector3 TargetExactPosition => _targetCollider ? _targetCollider.bounds.ClosestPoint(transform.position) : Vector3.zero;

    private void Awake()
    {
        _contactFilter = new ContactFilter2D
        {
            useLayerMask = true,
            layerMask = _targetLayer,
            useTriggers = true
        };
    }

    private void OnDisable()
    {
        CurrentTarget = null;
        _targetCollider = null;
    }

    private void Update()
    {
        _scanTimer += Time.deltaTime;

        if (_scanTimer >= _scanInterval)
        {
            _scanTimer = 0f;
            FindClosestTarget();
        }
    }

    private void FindClosestTarget()
    {
        
        int hitsCount = Physics2D.OverlapCircle(transform.position, _visionRadius, _contactFilter, _visionResults);

        if (hitsCount == 0)
        {
            CurrentTarget = null;
            _targetCollider = null;
            return;
        }

        ITarget bestTarget = null;
        Collider2D bestCollider = null;
        float closestDistanceSqr = float.PositiveInfinity;

        for (int i = 0; i < hitsCount; i++)
        {
            Collider2D candidateCollider = _visionResults[i];
            
            if (IsInBlindZone(candidateCollider.transform.position))
                continue;

            if (candidateCollider.TryGetComponent(out ITarget target))
            {
                Vector2 closestPoint = candidateCollider.bounds.ClosestPoint(transform.position);
                float dSqrToTarget = (closestPoint - (Vector2)transform.position).sqrMagnitude;

                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = target;
                    bestCollider = candidateCollider;
                }
            }
        }

        
        if (bestTarget != null)
        {
            CurrentTarget = bestTarget;
            _targetCollider = bestCollider;
            return;
        }

        if (CurrentTarget == null || ((Vector2)(CurrentTarget.Position - transform.position)).sqrMagnitude > _loseTargetRadius * _loseTargetRadius)
        {
            CurrentTarget = null;
            _targetCollider = null;
        }
    }

    private bool IsInBlindZone(Vector3 targetPosition)
    {
        if (_blindZoneHeight <= 0 || _blindZoneWidth <= 0) return false;
        
        Vector3 center = transform.position + (Vector3)_blindZoneOffset;
        float halfWidth = _blindZoneWidth / 2f;
        float halfHeight = _blindZoneHeight / 2f;

        bool insideX = targetPosition.x >= center.x - halfWidth && targetPosition.x <= center.x + halfWidth;
        bool insideY = targetPosition.y >= center.y - halfHeight && targetPosition.y <= center.y + halfHeight;

        return insideX && insideY;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _visionRadius);
        
        Gizmos.color = Color.orange;
        Gizmos.DrawWireSphere(transform.position, _loseTargetRadius);
        
        Vector3 center = transform.position + (Vector3)_blindZoneOffset;
        Gizmos.color = new Color(1f, 0f, 0f, 0.35f);
        Gizmos.DrawCube(center, new Vector3(_blindZoneWidth, _blindZoneHeight, 0f));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, new Vector3(_blindZoneWidth, _blindZoneHeight, 0f));
    }
}
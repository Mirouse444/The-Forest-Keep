using UnityEngine;

public class TargetScanner : MonoBehaviour
{
    [Header("Vision Configuration")]
    [SerializeField, Min(0)] private float _visionRadius = 5f;
    [SerializeField, Min(0)] private float _loseTargetRadius = 7f;
    [SerializeField, Min(0)] private float _scanInterval = 0.25f;
    [SerializeField] private LayerMask _targetLayer;

    private float _scanTimer;
    private Collider2D _targetCollider;
    private ContactFilter2D _contactFilter;
    private readonly Collider2D[] _visionResults = new Collider2D[25];
    
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
    
    private void FindClosestTarget() => CurrentTarget = GetClosestTarget();

    public ITarget GetClosestTarget(System.Predicate<ITarget> filter = null)
    {
        int hitsCount = Physics2D.OverlapCircle(transform.position, _visionRadius, _contactFilter, _visionResults);

        if (hitsCount == 0) 
        {
            _targetCollider = null;
            return null;
        }

        ITarget bestTarget = null;
        Collider2D bestCollider = null;
        float closestDistanceSqr = float.PositiveInfinity;

        for (int i = 0; i < hitsCount; i++)
        {
            Collider2D candidateCollider = _visionResults[i];
            
            if (candidateCollider.TryGetComponent(out ITarget target))
            {
                if (filter != null && !filter(target))
                    continue;

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

        _targetCollider = bestCollider;
        return bestTarget;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _visionRadius);
        
        Gizmos.color = Color.orange;
        Gizmos.DrawWireSphere(transform.position, _loseTargetRadius);
    }
}
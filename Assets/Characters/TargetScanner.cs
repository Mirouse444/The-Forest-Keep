using System.Collections;
using UnityEngine;

internal class TargetScanner : MonoBehaviour
{
    [Header("Vision Configuration")]
    [SerializeField] private float _visionRadius = 5f;
    [SerializeField] private float _loseTargetRadius = 7f;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _scanInterval = 0.25f;

    private WaitForSeconds _scanTimer;
    private Collider2D[] _visionResults = new Collider2D[10];
    private ContactFilter2D _contactFilter;

    public ITarget CurrentTarget { get; private set; }

    public Collider2D TargetCollider { get; private set; }

    public Vector3 TargetExactPosition
    {
        get
        {
            if (TargetCollider != null)
                return TargetCollider.ClosestPoint(transform.position);

            return CurrentTarget?.Position ?? transform.position;
        }
    }

    private void Awake()
    {
        _contactFilter = new ContactFilter2D
        {
            useLayerMask = true,
            layerMask = _targetLayer,
            useTriggers = true
        };

        _scanTimer = new WaitForSeconds(_scanInterval);
    }

    private void OnEnable() => StartCoroutine(Scan());

    private void OnDisable()
    {
        CurrentTarget = null;
        TargetCollider = null;
    }

    private IEnumerator Scan()
    {
        while (true)
        {
            FindClosestTarget();
            yield return _scanTimer;
        }
    }

    private void FindClosestTarget()
    {
        int hitsCount = Physics2D.OverlapCircle(transform.position, _visionRadius, _contactFilter, _visionResults);

        if (hitsCount == 0)
        {
            CurrentTarget = null;
            TargetCollider = null;
            return;
        }

        ITarget bestTarget = null;
        Collider2D bestCollider = null;
        float closestDistanceSqr = float.PositiveInfinity;

        for (int i = 0; i < hitsCount; i++)
        {
            if (_visionResults[i].TryGetComponent(out ITarget target))
            {
                Vector2 closestPoint = _visionResults[i].ClosestPoint(transform.position);
                float dSqrToTarget = (closestPoint - (Vector2)transform.position).sqrMagnitude;

                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = target;
                    bestCollider = _visionResults[i];
                }
            }
        }

        if (bestTarget != null && closestDistanceSqr >= _loseTargetRadius * _loseTargetRadius)
        {
            CurrentTarget = null;
            TargetCollider = null;
        }
        else
        {
            CurrentTarget = bestTarget;
            TargetCollider = bestCollider;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _visionRadius);
        Gizmos.color = Color.orange;
        Gizmos.DrawWireSphere(transform.position, _loseTargetRadius);
    }
}
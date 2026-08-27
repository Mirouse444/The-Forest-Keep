using UnityEngine;

public class ProjectileExplosion : MonoBehaviour
{
    [SerializeField] private Projectile _projectileRoot;
    [SerializeField] private ProjectileCollision _collisionDetector;
    [SerializeField] private LayerMask _damageableLayer;

    private readonly Collider2D[] _visionResults = new Collider2D[20];
    private ContactFilter2D _contactFilter;
    public CannonStats Stats { private get; set; }

    private void Awake()
    {
        _contactFilter = new ContactFilter2D
        {
            useLayerMask = true,
            layerMask = _damageableLayer,
            useTriggers = true
        };
    }

    private void OnEnable()
    {
        _collisionDetector.OnTargetHit += Explode;
        _collisionDetector.OnGroundHit += Explode;
    }

    private void OnDisable()
    {
        _collisionDetector.OnTargetHit -= Explode;
        _collisionDetector.OnGroundHit -= Explode;
    }

    private void Explode(Collider2D hitCollider)
    {
        if (Stats.ExplosionRadius <= 0.01f) return;

        Vector2 explosionCenter = transform.position;
        int hitsCount = Physics2D.OverlapCircle(transform.position, Stats.ExplosionRadius, _contactFilter, _visionResults);

        for(int i =  0; i < hitsCount; i++)
        {
            float distance = Vector2.Distance(explosionCenter, _visionResults[i].transform.position);
            
            float damagePercent = Mathf.Clamp01(1f - distance / Stats.ExplosionRadius);
            int calculatedDamage = Mathf.RoundToInt(Stats.Damage * damagePercent);

            if (_visionResults[i].TryGetComponent<IDamageable>(out var damageable))
                damageable.TakeDamage(new DamageInfo(calculatedDamage, false));

            if (_visionResults[i].TryGetComponent<IKnockbackable>(out var knockbackable))
            {
                Vector2 direction = ((Vector2)_visionResults[i].transform.position - explosionCenter).normalized;
                knockbackable.ApplyKnockback(direction * Stats.KnockbackForce * damagePercent);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (Stats && Stats.ExplosionRadius > 0)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, Stats.ExplosionRadius);
        }
    }
}
using UnityEngine;

public class ProjectileExplosion : MonoBehaviour
{
    [SerializeField] private Projectile _projectileRoot;
    [SerializeField] private ProjectileCollision _collisionDetector;
    [SerializeField] private LayerMask _damageableLayer;
    
    [Header("Explosion Settings")]
    [SerializeField] private float _defaultExplosionRadius = 2f;

    [Header("Visuals")]
    [SerializeField] private ExplosionEffectPoolSO _effectPoolSo;
    [SerializeField] private GameObject _explosionEffectPrefab;

    private readonly Collider2D[] _visionResults = new Collider2D[20];
    private ContactFilter2D _contactFilter;
    
    private LaunchData _launchData;
    private float _currentExplosionRadius;

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
        _projectileRoot.LaunchAction += OnLaunch;
        _collisionDetector.OnTargetHit += Explode;
        _collisionDetector.OnGroundHit += Explode;
    }

    private void OnDisable()
    {
        _projectileRoot.LaunchAction -= OnLaunch;
        _collisionDetector.OnTargetHit -= Explode;
        _collisionDetector.OnGroundHit -= Explode;
    }

    private void OnLaunch(LaunchData data)
    {
        _launchData = data;
        if (_currentExplosionRadius <= 0f)
            _currentExplosionRadius = _defaultExplosionRadius;
    }
    
    public void OverrideExplosionRadius(float newRadius)
    {
        _currentExplosionRadius = newRadius;
    }

    private void Explode(Collider2D hitCollider)
    {
        if (_currentExplosionRadius <= 0.01f) return;

        Vector2 explosionCenter = transform.position;
        int hitsCount = Physics2D.OverlapCircle(transform.position, _currentExplosionRadius, _contactFilter, _visionResults);

        _effectPoolSo.GetEffect.Play(_currentExplosionRadius, transform.position);
        
        for(int i = 0; i < hitsCount; i++)
        {
            float distance = Vector2.Distance(explosionCenter, _visionResults[i].transform.position);
            
            float damagePercent = Mathf.Clamp01(1f - distance / _currentExplosionRadius);
            
            int calculatedDamage = Mathf.RoundToInt(_launchData.WeaponDamage * damagePercent);

            if (_visionResults[i].TryGetComponent<IDamageable>(out var damageable))
                damageable.TakeDamage(new DamageInfo(calculatedDamage, _launchData.IsCritical));

            if (_visionResults[i].TryGetComponent<IKnockbackable>(out var knockbackable))
            {
                Vector2 direction = ((Vector2)_visionResults[i].transform.position - explosionCenter).normalized;
                knockbackable.ApplyKnockback(direction * _launchData.KnockbackForce * damagePercent);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _currentExplosionRadius > 0 ? _currentExplosionRadius : _defaultExplosionRadius);
    }
}
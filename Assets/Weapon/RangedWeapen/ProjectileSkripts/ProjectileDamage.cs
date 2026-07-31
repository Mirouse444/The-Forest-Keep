using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    [SerializeField] private Projectile _projectileRoot;
    [SerializeField] private ProjectileCollision _collision;
    [SerializeField] private int _baseProjectileDamage;

    private int _totalDamage;

    private void OnEnable()
    {
        _projectileRoot.LaunchAction += OnLaunch;
        _collision.OnTargetHit += DealDamage;
    }

    private void OnDisable()
    {
        _projectileRoot.LaunchAction -= OnLaunch;
        _collision.OnTargetHit -= DealDamage;
    }

    private void OnLaunch(LaunchData data)
    {
        _totalDamage = _baseProjectileDamage + data.WeaponDamage;
    }

    private void DealDamage(Collider2D target)
    {
        if (target.TryGetComponent<IDamageable>(out var damageable))
            damageable.TakeDamage(_totalDamage);
    }
}

using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    [SerializeField] private Projectile _projectileRoot;
    [SerializeField] private ProjectileCollision _collision;

    private LaunchData _data;

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

    private void OnLaunch(LaunchData data) => _data =  data;

    private void DealDamage(Collider2D target)
    {
        if (target.TryGetComponent<IDamageable>(out var damageable))
            damageable.TakeDamage(new DamageInfo(_data.WeaponDamage, _data.IsCritical));
    }
}

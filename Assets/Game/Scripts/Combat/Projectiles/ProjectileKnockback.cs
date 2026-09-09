using UnityEngine;

public class ProjectileKnockback : MonoBehaviour
{
    [SerializeField] private Projectile _projectileRoot;
    [SerializeField] private ProjectileCollision _collision;

    private const float MinKnockbackForce = 0.01f;
    private float _knockbackForce;

    private void OnEnable()
    {
        _projectileRoot.LaunchAction += OnLaunch;
        _collision.OnTargetHit += DealKnockback;
    }

    private void OnDisable()
    {
        _projectileRoot.LaunchAction -= OnLaunch;
        _collision.OnTargetHit -= DealKnockback;
    }

    private void OnLaunch(LaunchData data) => _knockbackForce = data.KnockbackForce;

    private void DealKnockback(Collider2D target)
    {
        if (_knockbackForce <= MinKnockbackForce) return;

        if (target.TryGetComponent<IKnockbackable>(out var knockbackable))
        {
            Vector2 hitDirection = ((Vector2)(target.transform.position - transform.position)).normalized;
            knockbackable.ApplyKnockback(hitDirection * _knockbackForce);
        }
    }
}
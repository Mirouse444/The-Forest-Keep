using UnityEngine;

public class CannonWeapon : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private ProjectilePool _projectilePool;

    private float _nextFireTime;

    public bool CanFire(float fireRate) => Time.time >= _nextFireTime;

    public void Fire(Vector2 direction, CannonStats stats)
    {
        _nextFireTime = Time.time + stats.FireRate;

        Projectile projectile = _projectilePool.GetProjectile();
        
        if (projectile.TryGetComponent<ProjectileExplosion>(out var explosion))
            explosion.Stats = stats;

        LaunchData launchData = new LaunchData(
            direction: direction, 
            weaponDamage: stats.Damage,
            speed: stats.ProjectileSpeed,
            knockbackForce: stats.KnockbackForce,
            isCritical: false
        );

        projectile.Launch(launchData, _firePoint.position, _firePoint.rotation);
    }
}
using UnityEngine;

public class CannonLauncher : MonoBehaviour, IWeaponLauncher
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private CannonInfo _info;
    [SerializeField] private ProjectilePoolSO _projectilePool;

    public event System.Action OnFire;

    public void Fire(Vector2 direction, float powerMultiplier = 1f)
    {
        Projectile projectile = _projectilePool.Spawner.Spawn;
        
        if (projectile.TryGetComponent<ProjectileExplosion>(out var explosion))
            explosion.OverrideExplosionRadius(_info.ExplosionRadius); 

        LaunchData launchData = new LaunchData
        (
            direction: direction, 
            weaponDamage: _info.Damage,
            speed: _info.ProjectileSpeed,
            knockbackForce: _info.KnockbackForce,
            isCritical: false
        );
        
        OnFire?.Invoke();
        projectile.Launch(launchData, _firePoint.position, _firePoint.rotation);
    }
}
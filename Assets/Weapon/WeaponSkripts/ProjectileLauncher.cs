using UnityEngine;

public class ProjectileLauncher : MonoBehaviour, IGunLauncher
{
    [SerializeField] private ProjectilePool _pool;
    [SerializeField] private Transform _shootPlace;
    [SerializeField, Min(0)] private int _damage;
    [SerializeField, Min(0)] private int _criticalDamage;
    [SerializeField, Range(0, 100)] private int _criticalChance;
    [SerializeField, Min(0)] private float _shootSpeed;
    [SerializeField, Min(0)] private float _knockbackForce;
    
    
    public float ShootSpeed => _shootSpeed;

    public void Fire(Vector2 direction, float powerMultiplier = 1)
    {
        bool isCritical = _criticalChance > Random.Range(0, 100);

        int baseDamage = isCritical ? _criticalDamage : _damage;

        LaunchData data = new LaunchData(
            direction.normalized,
            Mathf.RoundToInt(baseDamage * powerMultiplier),
            _shootSpeed * powerMultiplier,
            _knockbackForce * powerMultiplier,
            isCritical
        );

        _pool.GetProjectile().Launch(data, _shootPlace.position, _shootPlace.rotation);
    }
}
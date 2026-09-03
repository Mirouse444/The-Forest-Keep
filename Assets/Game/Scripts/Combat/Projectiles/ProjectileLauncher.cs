using UnityEngine; 

public class ProjectileLauncher : MonoBehaviour, IWeaponLauncher
{
    [SerializeField] private Transform _shootPlace;
    [SerializeField] private ProjectilePoolSO _projectileSpawner;
    [SerializeField, Min(0)] private int _minDamage;
    [SerializeField, Min(0)] private int _maxDamage;
    [SerializeField, Min(0)] private int _criticalDamage;
    [SerializeField, Range(0, 100)] private int _criticalChance;
    [SerializeField, Min(0)] private float _shootSpeed;
    [SerializeField, Min(0)] private float _knockbackForce;

    public float ShootSpeed => _shootSpeed;

    public void Fire(Vector2 direction, float powerMultiplier = 1)
    {
        bool isCritical = _criticalChance > Random.Range(0, 100);

        int baseDamage = isCritical ? _criticalDamage : Random.Range(_minDamage, _maxDamage + 1);

        LaunchData data = new LaunchData(
            direction.normalized,
            Mathf.RoundToInt(baseDamage * powerMultiplier),
            _shootSpeed * powerMultiplier,
            _knockbackForce * powerMultiplier,
            isCritical
        );

        _projectileSpawner.Spawner.Spawn.Launch(data, _shootPlace.position, _shootPlace.rotation);
    }
}
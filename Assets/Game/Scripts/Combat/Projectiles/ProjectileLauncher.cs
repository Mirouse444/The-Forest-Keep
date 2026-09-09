using UnityEngine; 

public class ProjectileLauncher : MonoBehaviour, IWeaponLauncher
{
    [SerializeField] private Transform _shootPlace;
    [SerializeField] private ProjectilePoolSO _projectileSpawner;
    [SerializeField] private WeaponInfoSo _info;

    public event System.Action OnFire;
    
    public void Fire(Vector2 direction, float powerMultiplier = 1)
    {
        bool isCritical = _info.CriticalChance > Random.Range(0, 100);

        int baseDamage = isCritical ? _info.CriticalDamage : Random.Range(_info.MinDamage, _info.MaxDamage + 1);

        LaunchData data = new LaunchData(
            direction.normalized,
            Mathf.RoundToInt(baseDamage * powerMultiplier),
            _info.ShootSpeed * powerMultiplier,
            _info.KnockbackForce * powerMultiplier,
            isCritical
        );

        OnFire?.Invoke();
        _projectileSpawner.Spawner.Spawn.Launch(data, _shootPlace.position, _shootPlace.rotation);
    }
}
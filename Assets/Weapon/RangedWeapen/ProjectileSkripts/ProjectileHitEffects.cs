using UnityEngine;

[RequireComponent(typeof(Projectile))]
[RequireComponent(typeof(ProjectileCollision))]
public class ProjectileHitEffects : MonoBehaviour
{
    private ProjectileCollision _collision;
    private IDamageTextSpawner _spawner;
    private LaunchData _currentData;
    private Projectile _projectile;

    public void Initialize(IDamageTextSpawner spawner) => _spawner = spawner;

    private void Awake()
    {
        _collision = GetComponent<ProjectileCollision>();
        _projectile = GetComponent<Projectile>();
    }

    private void OnEnable()
    {
        _collision.OnTargetHit += SpawnDamageText;
        _projectile.LaunchAction += SaveData;
    }

    private void OnDisable()
    {
        _collision.OnTargetHit -= SpawnDamageText;
        _projectile.LaunchAction -= SaveData;
    }

    private void SaveData(LaunchData data) => _currentData = data;

    private void SpawnDamageText(Collider2D targetCollider) => 
        _spawner?.SpawnText(targetCollider.transform.position, _currentData.WeaponDamage, _currentData.IsCritical);
}
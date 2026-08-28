using UnityEngine;

public class SpawnStuckProjectile : MonoBehaviour
{
    [SerializeField] private StuckProjectilePoolSO _spawner;
    [SerializeField] private ProjectileCollision _collision;
    
    private void OnEnable()
    {
        _collision.OnTargetHit += SpawnStuck;
        _collision.OnGroundHit += SpawnStuck;
    }

    private void OnDisable()
    {
        _collision.OnTargetHit -= SpawnStuck;
        _collision.OnGroundHit -= SpawnStuck;
    }

    private void SpawnStuck(Collider2D collider) => _spawner.Spawner.Spawn.Launch(transform, collider.transform);
}
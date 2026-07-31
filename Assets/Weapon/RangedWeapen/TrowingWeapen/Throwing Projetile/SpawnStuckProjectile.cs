using UnityEngine;

public class SpawnStuckProjectile : MonoBehaviour 
{
    [SerializeField] private ProjectileCollision _collision;
    [SerializeField] private StuckProjectilePool _pool;

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

    private void SpawnStuck(Collider2D collider)
    {
        _pool.GetStuckArrow().Init(transform, collider.transform);
    }
}
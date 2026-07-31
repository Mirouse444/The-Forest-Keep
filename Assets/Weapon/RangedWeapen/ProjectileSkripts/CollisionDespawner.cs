using UnityEngine;

[RequireComponent(typeof(Projectile))]
internal class CollisionDespawner : MonoBehaviour
{
    [SerializeField, Min(1)] private int _pierceCount;
    [SerializeField] private ProjectileCollision _collisionDetector;
    [SerializeField] private Projectile _projectileRoot;

    private int _currentPierceCount;

    private void OnEnable()
    {
        _currentPierceCount = _pierceCount;
        _collisionDetector.OnGroundHit += OnGroundHit;
        _collisionDetector.OnTargetHit += OnEnemyHit;
    }

    private void OnDisable()
    {
        _collisionDetector.OnGroundHit -= OnGroundHit;
        _collisionDetector.OnTargetHit -= OnEnemyHit;
    }

    private void OnEnemyHit(Collider2D enemy)
    {
        _currentPierceCount--;
        if (_currentPierceCount <= 0)
        {
            _projectileRoot.Despawn();
        }
    }

    private void OnGroundHit(Collider2D ground)
    {
        _projectileRoot.Despawn(); 
    }
}
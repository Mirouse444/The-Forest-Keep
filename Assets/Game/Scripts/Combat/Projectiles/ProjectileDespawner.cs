using UnityEngine;

[RequireComponent(typeof(Projectile))]
[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileDespawner : MonoBehaviour
{
    [SerializeField, Min(1)] private int _pierceCount;
    [SerializeField] private ProjectileCollision _collisionDetector;
    [SerializeField] private Projectile _projectileRoot;
    [SerializeField, Min(0)] private float _timeToDespawn = 1f;

    private float _timer;
    private int _currentPierceCount;

    private void OnEnable()
    {
        _currentPierceCount = _pierceCount;
        _collisionDetector.OnGroundHit += OnGroundHit;
        _collisionDetector.OnTargetHit += OnEnemyHit;
        _timer = 0;
    }

    private void OnDisable()
    {
        _collisionDetector.OnGroundHit -= OnGroundHit;
        _collisionDetector.OnTargetHit -= OnEnemyHit;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        
        if (_timer >= _timeToDespawn)
            _projectileRoot.Despawn();
    }

    private void OnEnemyHit(Collider2D enemy)
    {
        _currentPierceCount--;
        
        if (_currentPierceCount <= 0)
            _projectileRoot.Despawn();
    }

    private void OnGroundHit(Collider2D ground) => _projectileRoot.Despawn();
}
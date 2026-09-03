using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePool : MonoBehaviour, IProjectileSpawner
{
    [SerializeField] private ProjectilePoolSO _projectileSpawner;
    [SerializeField] private Projectile _projectilePrefab;
    
    private IObjectPool<Projectile> _projectilePool;

    private void Awake()
    {
        _projectileSpawner.Spawner = this;
        
        _projectilePool = new ObjectPool<Projectile>(
            createFunc: CreateProjectile,
            actionOnGet: OnTakeProjectileFromPool,
            actionOnRelease: OnReturnProjectileToPool,
            actionOnDestroy: OnDestroyProjectile,
            collectionCheck: false,
            defaultCapacity: 30,
            maxSize: 50
        );
    }

    public Projectile Spawn => _projectilePool.Get();

    private Projectile CreateProjectile()
    {
        Projectile projectile = Instantiate(_projectilePrefab, Vector3.zero, Quaternion.identity);
        projectile.SetReleaseAction(_projectilePool.Release);

        return projectile;
    }
    
    private void OnTakeProjectileFromPool(Projectile projectile) => projectile.gameObject.SetActive(true);
    private void OnReturnProjectileToPool(Projectile projectile) => projectile.gameObject.SetActive(false);
    private void OnDestroyProjectile(Projectile  projectile) => Destroy(projectile.gameObject);
}
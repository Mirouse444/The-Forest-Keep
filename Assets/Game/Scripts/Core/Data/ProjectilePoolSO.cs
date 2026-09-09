using UnityEngine;


[CreateAssetMenu(menuName = "Pool/Projectile/Projectile", fileName = "Projectile", order = 0)]
public class ProjectilePoolSO : ScriptableObject
{
    public IProjectileSpawner Spawner { get; set; }
}
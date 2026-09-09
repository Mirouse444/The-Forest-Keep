using UnityEngine;

[CreateAssetMenu(menuName = "Pool/Projectile/StuckProjectile", fileName = "StuckProjectile", order = 0)]
public class StuckProjectilePoolSO : ScriptableObject
{
    public IStuckProjectileSpawner Spawner { get; set; }
}
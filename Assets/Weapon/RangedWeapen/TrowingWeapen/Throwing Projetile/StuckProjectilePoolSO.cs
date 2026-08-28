using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Projectile/StuckProjectile", fileName = "Pools", order = 0)]
public class StuckProjectilePoolSO : ScriptableObject
{
    public IStuckProjectileSpawner Spawner { get; set; }
}
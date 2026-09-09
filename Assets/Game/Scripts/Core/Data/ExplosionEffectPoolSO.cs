using UnityEngine;

[CreateAssetMenu(menuName = "Pool/Projectile/ExplosionEffect", fileName = "ExplosionEffectPoolSO", order = 0)]
public class ExplosionEffectPoolSO : ScriptableObject
{
    public ExplosionEffectPool PoolSetter { private get; set; }

    public ExplosionEffect GetEffect => PoolSetter.GetEffect();
}
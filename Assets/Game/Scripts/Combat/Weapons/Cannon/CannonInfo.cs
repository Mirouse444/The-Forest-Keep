using UnityEngine;

[CreateAssetMenu(fileName = "CannonStats", menuName = "Weapon/Cannon Stats")]
public class CannonInfo : ScriptableObject
{
    [Header("Base Stats")]
    [SerializeField, Min(0)] private int _damage; 
    [SerializeField, Min(0)] private float _projectileSpeed = 15f;
    [SerializeField, Min(0)] private float _knockbackForce = 5f;

    [Header("Explosion")]
    [SerializeField, Min(0)] private float _explosionRadius ; 
    
    public float ProjectileSpeed => _projectileSpeed;
    public float KnockbackForce => _knockbackForce;
    public int Damage => _damage;
    public float ExplosionRadius => _explosionRadius;
}
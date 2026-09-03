using UnityEngine;

[CreateAssetMenu(fileName = "CannonStats", menuName = "Tower/Cannon Stats")]
public class CannonStats : ScriptableObject
{
    [Header("Base Stats")]
    public int Damage = 50;
    public float ProjectileSpeed = 15f;
    public float KnockbackForce = 5f;
    public float FireRate = 1.5f;

    [Header("Explosion (Level Progression)")]
    [Tooltip("0 = 1-й уровень (нет взрыва), > 0 = 2-й уровень и выше")]
    public float ExplosionRadius = 0f; 
}
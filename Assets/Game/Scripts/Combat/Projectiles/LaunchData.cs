using UnityEngine;

public readonly struct LaunchData
{
    public readonly Vector2 Direction;
    public readonly int WeaponDamage;
    public readonly float Speed;
    public readonly float KnockbackForce;
    public readonly bool IsCritical;

    public LaunchData(Vector2 direction, int weaponDamage, float speed, float knockbackForce, bool isCritical)
    {
        Direction = direction.normalized;
        WeaponDamage = Mathf.Max(0, weaponDamage);
        Speed = Mathf.Max(0, speed);
        KnockbackForce = Mathf.Max(0, knockbackForce);
        IsCritical = isCritical;
    }
}
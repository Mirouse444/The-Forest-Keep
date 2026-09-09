using UnityEngine;

public readonly struct LaunchData
{
    public Vector2 Direction { get; }
    public int WeaponDamage { get; }
    public float Speed { get; }
    public float KnockbackForce { get; }
    public bool IsCritical { get; }

    public LaunchData(Vector2 direction, int weaponDamage, float speed, float knockbackForce, bool isCritical)
    {
        Direction = direction.normalized;
        WeaponDamage = Mathf.Max(0, weaponDamage);
        Speed = Mathf.Max(0, speed);
        KnockbackForce = Mathf.Max(0, knockbackForce);
        IsCritical = isCritical;
    }
}
using UnityEngine;

public readonly struct MeleeStrikeData
{
    public readonly Vector2 Direction;
    public readonly Vector2 PushDirection;
    public readonly int Damage;
    public readonly float KnockbackForce;
    public readonly float AttackTime;
    public readonly bool IsCritical;

    public MeleeStrikeData(Vector2 direction, Vector2 pushDirection, int damage, float knockbackForce, float attackTime, bool isCritical)
    {
        Direction = direction.normalized;
        PushDirection = pushDirection.normalized;
        Damage = Mathf.Max(0 , damage);
        KnockbackForce = Mathf.Max(0, knockbackForce);
        AttackTime = Mathf.Max(0, attackTime);
        IsCritical = isCritical;
    }
}
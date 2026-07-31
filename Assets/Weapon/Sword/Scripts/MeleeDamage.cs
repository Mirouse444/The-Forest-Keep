using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    [SerializeField] private MeleeCollision _collision;

    private void OnEnable() => _collision.OnTargetHit += DealDamage;
    private void OnDisable() => _collision.OnTargetHit -= DealDamage;

    private void DealDamage(Collider2D target, MeleeStrikeData data)
    {
        if (target.TryGetComponent<IDamageable>(out var damageable))
            damageable.TakeDamage(data.Damage);
    }
}
using UnityEngine;

public class MeleeKnockback : MonoBehaviour
{
    [SerializeField] private MeleeCollision _collision;

    private void OnEnable() => _collision.OnTargetHit += DealKnockback;
    private void OnDisable() => _collision.OnTargetHit -= DealKnockback;

    private void DealKnockback(Collider2D target, MeleeStrikeData data)
    {
        if (target.TryGetComponent<IKnockbackable>(out var knockbackable))
            knockbackable.ApplyKnockback(data.PushDirection * data.KnockbackForce);
    }
}
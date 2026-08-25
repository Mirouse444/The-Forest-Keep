using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private DamageOnPlace _damageOnPlace;
    [SerializeField] private HealthComponent _health;

    private void OnEnable()
    {
        _health.State.OnApplyDamage += PlayHurtAnimation;
        _health.State.OnDeath += PlayDeathAnimation;
        _damageOnPlace.OnTakeDamage += PlayTakeDamageAnimation;
    }

    private void OnDisable()
    {
        _health.State.OnApplyDamage -= PlayHurtAnimation;
        _health.State.OnDeath -= PlayDeathAnimation;
        _damageOnPlace.OnTakeDamage -= PlayTakeDamageAnimation;
    }

    private void PlayHurtAnimation() => _animator.SetTrigger("Hurt");
    private void PlayDeathAnimation() => _animator.SetTrigger("Death");
    private void PlayTakeDamageAnimation() => _animator.SetTrigger("Attack");
}
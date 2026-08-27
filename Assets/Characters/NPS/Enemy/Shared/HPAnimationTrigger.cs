using UnityEngine;

public class HPAnimationTrigger : MonoBehaviour
{
    [SerializeField] private DeathAnimationSO _deathAnimation;
    [SerializeField] private HealthComponent _health;
    [SerializeField] private Animator _animator;
    
    private void OnEnable()
    {
        _health.State.OnApplyDamage += PlayHurtAnimation;
        _health.State.OnDeath += PlayDeathAnimation;
    }

    private void OnDisable()
    {
        _health.State.OnApplyDamage -= PlayHurtAnimation;
        _health.State.OnDeath -= PlayDeathAnimation;
    }

    private void PlayHurtAnimation() => _animator.SetTrigger("Hurt");
    private void PlayDeathAnimation() => _deathAnimation.DeathAnimation.SpawnDeathModel(transform.position, transform.rotation);
}
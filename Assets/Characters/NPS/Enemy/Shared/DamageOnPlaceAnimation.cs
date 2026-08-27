using UnityEngine;

public class DamageOnPlaceAnimation : MonoBehaviour
{
    [SerializeField] private DamageOnPlace _damageOnPlace;
    [SerializeField] private Animator _animator;
    
    private void OnEnable() => _damageOnPlace.OnTakeDamage += PlayTakeDamageAnimation;
    private void OnDisable() => _damageOnPlace.OnTakeDamage -= PlayTakeDamageAnimation;
    
    private void PlayTakeDamageAnimation() => _animator.SetTrigger("Attack");
}
using UnityEngine;

public class HurtAudio : MonoBehaviour
{
    [SerializeField] private HealthComponent _health;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _actionSound;

    private void OnEnable() => _health.State.OnApplyDamage += PlayAudio;
    private void OnDisable() => _health.State.OnApplyDamage -= PlayAudio;

    private void PlayAudio() => _audioSource.PlayOneShot(_actionSound);
}

using UnityEngine;

public class SlimeAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _actionSound;
    [SerializeField] private SlimeMover _mover;

    private void OnEnable() => _mover.OnJump += PlayAudio;
    private void OnDisable() => _mover.OnJump -= PlayAudio;

    private void PlayAudio() => _audioSource.PlayOneShot(_actionSound);
}
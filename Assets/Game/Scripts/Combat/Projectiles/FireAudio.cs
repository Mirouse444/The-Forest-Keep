using UnityEngine;

public class FireAudio : MonoBehaviour
{
    [SerializeField]  private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private ProjectileLauncher _projectileLauncher;

    private void OnEnable() => _projectileLauncher.OnFire += PlayClip;
    private void OnDisable() => _projectileLauncher.OnFire -= PlayClip;
    
    private void PlayClip() => _audioSource.PlayOneShot(_audioClip);
}
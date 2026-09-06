using UnityEngine;

public class ReloadAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private StandardMagazine _magazine;

    private void OnEnable() => _magazine.OnReloadStarted += PlayClip;
    private void OnDisable() => _magazine.OnReloadStarted -= PlayClip;
    
    private void PlayClip(float ctx) => _audioSource.PlayOneShot(_audioClip);
}
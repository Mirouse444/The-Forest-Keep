using UnityEngine;

public class DryFireAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private StandardMagazine _standardMagazine;

    private void OnEnable() => _standardMagazine.OnDryFire += PlayClip;
    private void OnDisable() => _standardMagazine.OnDryFire -= PlayClip;

    private void PlayClip() => _audioSource.PlayOneShot(_audioClip);
}
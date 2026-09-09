using UnityEngine;
using UnityEngine.Serialization;

public class CannonAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clip;
    [FormerlySerializedAs("_cannonWeapon")] [SerializeField] private CannonLauncher _cannonLauncher;

    private void OnEnable() => _cannonLauncher.OnFire += PlayClip;
    private void OnDisable() => _cannonLauncher.OnFire -= PlayClip;
    
    private void PlayClip() => _audioSource.PlayOneShot(_clip);
}
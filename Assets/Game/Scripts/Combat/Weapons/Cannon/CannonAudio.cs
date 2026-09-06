using UnityEngine;

public class CannonAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private CannonWeapon _cannonWeapon;

    private void OnEnable() => _cannonWeapon.OnFire += PlayClip;
    private void OnDisable() => _cannonWeapon.OnFire -= PlayClip;
    
    private void PlayClip() => _audioSource.PlayOneShot(_clip);
}
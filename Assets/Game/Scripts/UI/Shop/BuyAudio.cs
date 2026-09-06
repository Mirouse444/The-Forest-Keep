using UnityEngine;

public class BuyAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _buyClip;
    [SerializeField] private BuyButton[] _buyButtons;

    private void OnEnable()
    {
        foreach (var button in _buyButtons)
            if (button != null)
                button.OnBuy += PlayClip;
    }

    private void OnDisable()
    {
        foreach (var button in _buyButtons)
            if (button != null)
                button.OnBuy -= PlayClip;
    }
    
    private void PlayClip() => _audioSource.PlayOneShot(_buyClip);
}
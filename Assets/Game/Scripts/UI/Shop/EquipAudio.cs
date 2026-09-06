using UnityEngine;

public class EquipAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _equipClip;
    [SerializeField] private EquipButton[] _equipButtons;

    private void OnEnable()
    {
        foreach (var button in _equipButtons)
            if (button != null)
                button.OnEquip += PlayClip;
    }

    private void OnDisable()
    {
        foreach (var button in _equipButtons)
            if (button != null)
                button.OnEquip -= PlayClip;
    }
    
    private void PlayClip() => _audioSource.PlayOneShot(_equipClip);
}
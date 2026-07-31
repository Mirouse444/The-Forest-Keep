using UnityEngine;

public class AudioMuter : MonoBehaviour
{
    [SerializeField] private GameObject _cross;

    private bool _isMuted;


    public void ToggleMute()
    {
        _isMuted = !_isMuted;

        AudioListener.pause = _isMuted;

        if(_isMuted)
            _cross.SetActive(true);
        else
            _cross.SetActive(false);
    }
}
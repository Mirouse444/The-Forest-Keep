using UnityEngine;

public class MobAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private float _timeDistance;
    
    private float _timer;

    private void Update()
    {
        if (_source.isPlaying) return;
        
        _timer += Time.deltaTime;

        if (_timer >= _timeDistance)
        {
            _source.PlayOneShot(_audioClip);
            _timer = 0;
        }
    }
}

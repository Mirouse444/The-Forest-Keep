using UnityEngine;

public class ExplosionAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _clip;
    
    public System.Action<ExplosionAudio> ReleaseAction { private get; set; }
    
    private void OnEnable() => _source.PlayOneShot(_clip);
    
    private void LateUpdate()
    {
        if(!_source.isPlaying)
            ReleaseAction.Invoke(this);
    }
}
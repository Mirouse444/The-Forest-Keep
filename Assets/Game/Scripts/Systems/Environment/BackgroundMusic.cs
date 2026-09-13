using System.Collections;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private AudioClip[] _dayTracks;
    [SerializeField] private AudioClip[] _nightTracks;
    [SerializeField] private float _fadeTime = 2.0f;

    [SerializeField] private AudioSource _sourceOne;
    [SerializeField] private AudioSource _sourceTwo;
    
    [SerializeField] private DayTrigger _dayTrigger;
    [SerializeField] private NightTrigger _nightTrigger;

    private AudioSource _activeSource;
    private AudioSource _idleSource;
    
    private int _dayIndex;
    private int _nightIndex;
    
    private enum TimeState { Day, Night }
    private TimeState _currentState;

    private void Awake()
    {
        _activeSource = _sourceOne;
        _idleSource = _sourceTwo;
        
        _activeSource.volume = 0f;
        _idleSource.volume = 0f;
    }

    private void OnEnable()
    {
        _dayTrigger.OnDayStarted.AddListener(SwitchToDay);
        _nightTrigger.OnNightStarted.AddListener(SwitchToNight);
    }
    
    private void OnDisable()
    {
        _dayTrigger.OnDayStarted.RemoveListener(SwitchToDay);
        _nightTrigger.OnNightStarted.RemoveListener(SwitchToNight);
    }

    private void Start() => SwitchToDay();
        
    private void SwitchToDay()
    {
        _currentState = TimeState.Day;
        ChangeTrack();
    }

    private void SwitchToNight()
    {
        _currentState = TimeState.Night;
        ChangeTrack();
    }

    private void ChangeTrack()
    {
        StopAllCoroutines(); 
        
        SwapSources();
        
        AudioClip nextClip = GetNextClip();
        _activeSource.clip = nextClip;
        _activeSource.Play();

        StartCoroutine(CrossfadeRoutine());
        StartCoroutine(WaitTrackEndRoutine(nextClip.length));
    }

    private void SwapSources() => (_activeSource, _idleSource) = (_idleSource, _activeSource);

    private AudioClip GetNextClip()
    {
        if (_currentState == TimeState.Day)
        {
            AudioClip clip = _dayTracks[_dayIndex];
            _dayIndex = (_dayIndex + 1) % _dayTracks.Length;
            return clip;
        }
        else
        {
            AudioClip clip = _nightTracks[_nightIndex];
            _nightIndex = (_nightIndex + 1) % _nightTracks.Length;
            return clip;
        }
    }

    private IEnumerator CrossfadeRoutine()
    {
        float timer = 0f;
        float startIdleVol = _idleSource.volume;

        while (timer < _fadeTime)
        {
            timer += Time.unscaledDeltaTime;
            _activeSource.volume = Mathf.Lerp(0f, 1f, timer / _fadeTime);
            _idleSource.volume = Mathf.Lerp(startIdleVol, 0f, timer / _fadeTime);
            yield return null;
        }

        _activeSource.volume = 1f;
        _idleSource.volume = 0f;
        _idleSource.Stop();
    }

    private IEnumerator WaitTrackEndRoutine(float clipLength)
    {
        yield return new WaitForSecondsRealtime(clipLength - _fadeTime);
        
        ChangeTrack(); 
    }
}
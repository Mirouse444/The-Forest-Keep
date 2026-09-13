using Cysharp.Threading.Tasks;
using System.Threading;
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
    private CancellationTokenSource _trackCts;

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

        CancelCurrentTrack();
    }
    
    private void CancelCurrentTrack()
    {
        if(_trackCts == null) return;
        
        _trackCts.Cancel();
        _trackCts.Dispose();
        _trackCts = null;
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
        CancelCurrentTrack();
        _trackCts = new CancellationTokenSource();
        
        SwapSources();
        
        AudioClip nextClip = GetNextClip();
        _activeSource.clip = nextClip;
        _activeSource.Play();

        CrossfadeRoutine(_trackCts.Token).Forget();
        WaitTrackEndRoutine(nextClip.length, _trackCts.Token).Forget();
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

    private async UniTaskVoid CrossfadeRoutine(CancellationToken token)
    {
        float timer = 0f;
        float startIdleVol = _idleSource.volume;

        try
        {
            while (timer < _fadeTime)
            {
                timer += Time.unscaledDeltaTime;
                _activeSource.volume = Mathf.Lerp(0f, 1f, timer / _fadeTime);
                _idleSource.volume = Mathf.Lerp(startIdleVol, 0f, timer / _fadeTime); 
                await UniTask.Yield(cancellationToken: token);
            }

            _activeSource.volume = 1f;
            _idleSource.volume = 0f;
            _idleSource.Stop();
        }
        catch (System.OperationCanceledException) { }
    }

    private async UniTaskVoid WaitTrackEndRoutine(float clipLength, CancellationToken token)
    {
        try
        {
            float waitTime = Mathf.Max(0f, clipLength - _fadeTime);
            int startFadeTimeMs = (int)(waitTime * 1000);
            
            await UniTask.Delay(startFadeTimeMs, cancellationToken: token, ignoreTimeScale: true);

            ChangeTrack();
        }
        catch (System.OperationCanceledException) { }
    }
}
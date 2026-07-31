using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private Transform _sun;
    [SerializeField] private Transform _moon;

    [Space, Header("Day, night in second")]
    [SerializeField] private float _solarTimeDurationInSeconds;
    [SerializeField] private float _nighttimeDurationInSeconds;

    [Space, Header("Light")]
    [SerializeField] private Light2D _globalLight;
    [SerializeField] private Gradient _lightDayColorGradient;
    [SerializeField] private Gradient _lightNightColorGradient;

    [Space, Header("Trajectory")]
    [SerializeField] private AnimationCurve _heightCurve;
    [SerializeField] private float _maxHeight;
    [SerializeField] private float _arcWidth;

    public float CurrentTime { get; private set; }

    private float _day;
    private bool _isNight;

    private void Awake()
    {
        CurrentTime = _nighttimeDurationInSeconds;
        _day = _solarTimeDurationInSeconds + _nighttimeDurationInSeconds; 
        _isNight = false;
    }

    private void Start()
    {
    }

    private void Update()
    {
        CurrentTime = (CurrentTime + Time.deltaTime) % _day;

        if(CurrentTime < _nighttimeDurationInSeconds)
        {
            if (_isNight == false)
                SetMoon();

            float progress = CurrentTime / _nighttimeDurationInSeconds;

            CelestialBodyMove(_moon, progress);
            NightColorShift(progress); ;
        }
        else
        {
            if (_isNight)
                SetSun();

            float progress = (CurrentTime - _nighttimeDurationInSeconds) / _solarTimeDurationInSeconds;

            CelestialBodyMove(_sun, progress);
            SolarColorShift(progress);
        }
    }

    private void NightColorShift(float progress)
    {
        _globalLight.color = _lightNightColorGradient.Evaluate(progress);
    }

    private void SolarColorShift(float progress)
    {
        _globalLight.color = _lightDayColorGradient.Evaluate(progress);
    }

    private void CelestialBodyMove(Transform celestialBody, float progress)
    {
        float localX = Mathf.Lerp(-_arcWidth / 2f, _arcWidth / 2f, progress);
        float localY = _maxHeight * _heightCurve.Evaluate(progress);

        celestialBody.localPosition = new Vector3(localX, localY, celestialBody.localPosition.z);
    }

    private void SetMoon()
    {
        _moon.gameObject.SetActive(true);
        _sun.gameObject.SetActive(false);
        _isNight = true;
    }

    private void SetSun()
    {
        _sun.gameObject.SetActive(true);
        _moon.gameObject.SetActive(false);
        _isNight = false;
    }
}
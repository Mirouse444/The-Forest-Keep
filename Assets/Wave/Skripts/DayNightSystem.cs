using System;
using UnityEngine.Rendering.Universal;
using System.Collections;
using UnityEngine;

public class DayNightSystem : MonoBehaviour
{
    [SerializeField] private Transform _sun;
    [SerializeField] private Transform _moon;

    [Space, Header("Light")]
    [SerializeField] private Light2D _globalLight;
    [SerializeField] private Gradient _lightDayColorGradient;
    [SerializeField] private Gradient _lightNightColorGradient;
   
    
    [Space, Header("Time")]
    [SerializeField] private float _dayChangeTime;
    [SerializeField] private float _nightChangeTime;

    [Space, Header("Sun Trajectory")]
    [SerializeField] private AnimationCurve _sunHeightCurve;
    [SerializeField] private float _maxSunHeight;
    [SerializeField] private float _startSunPositionX;
    [SerializeField] private float _endSunPositionX;
    
    
    [Space, Header("Moon Trajectory")]
    [SerializeField] private AnimationCurve _moonHeightCurve;
    [SerializeField] private float _maxMoonHeight;
    [SerializeField] private float _startMoonPositionX;
    [SerializeField] private float _endMoonPositionX;
    
    [Space, Header("Triggers")]
    [SerializeField] private DayTrigger _dayStart;
    [SerializeField] private NightTrigger _nightStart;
    
    private Coroutine _celestialBodyMoveCoroutine;

    private void Start() => StartDay();

    private void OnEnable()
    {
        _dayStart.OnDayStarted += StartDay;
        _nightStart.OnNightStarted += StartNight;
    }
    
    private void OnDisable()
    {
        _dayStart.OnDayStarted -= StartDay;
        _nightStart.OnNightStarted -= StartNight;
    }

    private void StartDay()
    {
        _sun.gameObject.SetActive(true);
        _moon.gameObject.SetActive(false);
        
        if (_celestialBodyMoveCoroutine != null)
            StopCoroutine(_celestialBodyMoveCoroutine);
        
        _celestialBodyMoveCoroutine = StartCoroutine(CelestialBodyMove(_sun, _startSunPositionX, _endSunPositionX, _maxSunHeight, _dayChangeTime, _sunHeightCurve, _lightDayColorGradient));
    }

    private void StartNight()
    {
        _moon.gameObject.SetActive(true);
        _sun.gameObject.SetActive(false);
        
        if (_celestialBodyMoveCoroutine != null)
            StopCoroutine(_celestialBodyMoveCoroutine);
        
        _celestialBodyMoveCoroutine = StartCoroutine(CelestialBodyMove(_moon, _startMoonPositionX, _endMoonPositionX, _maxMoonHeight,  _nightChangeTime, _moonHeightCurve, _lightNightColorGradient));
    }

    private IEnumerator CelestialBodyMove(Transform celestialBody,float startPositionX, float endPositionX, float maxHeight, float time, AnimationCurve curve, Gradient gradient)
    {
        float timer = 0;
        
        while (timer <  time)
        {
            float progress = timer / time;
            
            float positionX = Mathf.Lerp(startPositionX, endPositionX, progress);
            float positionY = maxHeight * curve.Evaluate(progress);
            
            _globalLight.color = gradient.Evaluate(progress);
            celestialBody.position = new Vector3(positionX, positionY, celestialBody.position.z);
            timer +=  Time.deltaTime;
            
            yield return null;
        }
    }
}
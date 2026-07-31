using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightSystem : MonoBehaviour
{
    [SerializeField] private Transform _sun;
    [SerializeField] private Transform _moon;

    [Space, Header("Light")]
    [SerializeField] private Light2D _globalLight;
    [SerializeField] private Gradient _lightNightColorGradient;
    [SerializeField] private float _colorChangeTime;

    [Space, Header("Trajectory")]
    [SerializeField] private AnimationCurve _heightCurve;
    [SerializeField] private float _maxHeight;
    [SerializeField] private float _arcWidth;

    [Space, Header("Triggers")]
    [SerializeField] private DayTrigger _dayStart;
    [SerializeField] private NightTrigger _NightStart;

    private void OnEnable()
    {
        _dayStart.OnDayStarted += OnDayStarted;
    }

    private void OnDisable()
    {
        _dayStart.OnDayStarted -= OnDayStarted;
    }


    private void OnDayStarted()
    {

    }
}
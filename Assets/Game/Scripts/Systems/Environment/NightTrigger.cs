using System;
using UnityEngine;

public class NightTrigger : MonoBehaviour
{
    [SerializeField] private DayTrigger _dayTrigger;
    [SerializeField] private ActiveZone _activeZone;
    [SerializeField] private PlayerInputController _controller;
    
    private bool _isDay = true;
    
    public event Action OnNightStarted;

    private void OnEnable()
    {
        _controller.OnInteract += StartNight;
        _dayTrigger.OnDayStarted += StartDay;
    }

    private void OnDisable()
    {
        _controller.OnInteract -= StartNight;
        _dayTrigger.OnDayStarted -= StartDay;
    }

    private void StartDay() => _isDay = true; 
    
    public void StartNight()
    {
        if (_isDay && _activeZone.PlayerInZone)
        {
            OnNightStarted?.Invoke();
            _isDay = false;
        }
    }
}
using System;
using UnityEngine;

public class NightTrigger : MonoBehaviour
{
    [SerializeField] private DayTrigger _dayTrigger;
    [SerializeField] private ActiveZone _activeZone;
    
    private bool _isDay = true;
    
    public event Action OnNightStarted;

    private void OnEnable() =>_dayTrigger.OnDayStarted += () => _isDay = true;


    public void StartNight()
    {
        if (_isDay && _activeZone.PlayerInZone)
        {
            OnNightStarted?.Invoke();
            _isDay = false;
        }
    }
}
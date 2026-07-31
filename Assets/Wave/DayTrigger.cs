using System;
using UnityEngine;

public class DayTrigger : MonoBehaviour
{
    public event Action OnDayStarted;

    public void StartDay()
    {
        OnDayStarted.Invoke();
    }
}
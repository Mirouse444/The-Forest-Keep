using UnityEngine;

public class Victory : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _shop;
    [SerializeField] private DayTrigger _trigger;
    [SerializeField] private int _dayCount = 7;
    
    private int _currentDay;
    
    private void OnEnable() => _trigger.OnDayStarted.AddListener(AddDay);
    private void OnDisable() => _trigger.OnDayStarted.RemoveListener(AddDay);

    private void AddDay()
    {
        _currentDay++;
        
        if (_currentDay == _dayCount)
        {
            Destroy(_shop);
            _panel.SetActive(true);
        }
    }
}
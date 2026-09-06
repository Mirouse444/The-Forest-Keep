using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private DayTrigger _trigger;
    [SerializeField] private int _dayCount = 7;
    
    private int _currentDay;
    
    private void OnEnable() => _trigger.OnDayStarted += AddDay;
    private void OnDisable() => _trigger.OnDayStarted -= AddDay;

    private void AddDay()
    {
        _currentDay++;
        
        if (_currentDay == _dayCount)
            _panel.SetActive(true);
    }

    public void RestartGame()
    {
        _panel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        _panel.SetActive(false);
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
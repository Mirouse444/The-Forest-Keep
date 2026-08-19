using UnityEngine;

public class FirstNightDiology : MonoBehaviour
{
    [SerializeField] private TextDialogeButton _dialog;
    [SerializeField] private GameObject _border;
    [SerializeField] private NightTrigger _trigger;

    private void OnEnable()
    {
        _trigger.OnNightStarted += StaetDiology;
        _dialog.OnDialogeEnd += CloseDialoge;
    }

    private void OnDisable()
    {
        _trigger.OnNightStarted -= StaetDiology;
        _dialog.OnDialogeEnd -= CloseDialoge;
    }

    private void StaetDiology()
    {
        _dialog.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    
    private void CloseDialoge()
    {
        Destroy(_border);
        Destroy(_dialog.gameObject);
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}

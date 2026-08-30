using UnityEngine;

public class BordersController : MonoBehaviour
{
    [SerializeField] private TextDialogeButton _startBorderUI;
    [SerializeField] private TextDialogeButton _edgeBorderUI;
    [SerializeField] private GameObject _startBorders;
    [SerializeField] private NightTrigger _trigger;
    [SerializeField] private GameObject _panel;

    private TextDialogeButton _currentBorders;
    
    private void Start()
    {
        _trigger.OnNightStarted += DeleteStartBorders;
        _currentBorders = _startBorderUI;
    }

    private void OnEnable()
    {
        AddCloseEvent(_startBorderUI);
        AddCloseEvent(_startBorderUI);
    }

    private void OnDisable()
    {
        RemoveCloseEvent(_startBorderUI);
        RemoveCloseEvent(_edgeBorderUI);
    }

    private void RemoveCloseEvent(TextDialogeButton button)
    {
        if(button != null)
            button.OnDialogeEnd -= Close;
    }
    
    private void AddCloseEvent(TextDialogeButton button)
    {
        if (button != null)
            button.OnDialogeEnd += Close;
    }

    private void DeleteStartBorders()
    {
        Destroy(_startBorders);
        Destroy(_startBorders);
        _currentBorders = _edgeBorderUI;
        _edgeBorderUI.gameObject.SetActive(true);
        _trigger.OnNightStarted -= DeleteStartBorders;
    }

    private void Close()
    {
        _panel.SetActive(false);
        _currentBorders.gameObject.SetActive(true);
    }
}
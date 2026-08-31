using UnityEngine;

public class MenuPanelController : MonoBehaviour
{
    [SerializeField] private UIInputDetector _inputDetector;
    [SerializeField] private GameObject _panel;

    private void OnEnable() => _inputDetector.CallMenu += OnCall;

    private void OnDisable() => _inputDetector.CallMenu -= OnCall;

    public void OnCall()
    {
        if (_panel.activeInHierarchy)
            Close();
        else
            Open();
    }

    private void Open() => _panel.SetActive(true);
    private void Close() => _panel.SetActive(false);
}
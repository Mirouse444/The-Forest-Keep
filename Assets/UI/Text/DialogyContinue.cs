using UnityEngine.UI;
using UnityEngine;

public class DialogyContinue : MonoBehaviour
{
    [SerializeField] private GameObject _dialog;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _button;

    private void OnEnable() => _button.onClick.AddListener(OnButtonClick);
    private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);
    
    private void OnButtonClick()
    {
        _panel.SetActive(true);
        _dialog.SetActive(true);
        Destroy(gameObject);
    }
}
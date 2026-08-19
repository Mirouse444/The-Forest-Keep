using UnityEngine.UI;
using UnityEngine;

public class DiologyContinue : MonoBehaviour
{
    [SerializeField] private GameObject _diolog;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _button;

    private void OnEnable() => _button.onClick.AddListener(OnButtonClick);
    private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);
    
    private void OnButtonClick()
    {
        _panel.SetActive(true);
        _diolog.SetActive(true);
        Destroy(gameObject);
    }
}
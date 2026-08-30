using UnityEngine;
using UnityEngine.Serialization;

public class OpenShopOnFirstDiologEnd : MonoBehaviour
{
    [SerializeField] private GameObject _shop;
    [SerializeField] private TextDialogeButton _dialog;

    private void Start() => Time.timeScale = 0;

    private void OnEnable() => _dialog.OnDialogeEnd += OnDialogEnd;
    private void OnDisable() => _dialog.OnDialogeEnd -= OnDialogEnd;

    private void OnDialogEnd()
    {
        _shop.SetActive(true);
        Destroy(_dialog.gameObject);
        gameObject.SetActive(false);
    }
}
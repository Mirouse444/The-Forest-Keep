using UnityEngine;

public class OpenShopOnFirstDiologEnd : MonoBehaviour
{
    [SerializeField] private GameObject _shop;
    [SerializeField] private TextDialogeButton _diolog;

    private void Start() => Time.timeScale = 0;

    private void OnEnable() => _diolog.OnDialogeEnd += OnDiologEnd;
    private void OnDisable() => _diolog.OnDialogeEnd -= OnDiologEnd;

    private void OnDiologEnd()
    {
        _shop.SetActive(true);
        Destroy(_diolog.gameObject);
        gameObject.SetActive(false);
    }
}
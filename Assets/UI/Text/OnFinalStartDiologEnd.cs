using UnityEngine;

public class OnFinalStartDiologEnd : MonoBehaviour
{
    [SerializeField] private TextDialogeButton _dialoge;

    private void OnEnable() => _dialoge.OnDialogeEnd += OnDiologeEnd;
    private void OnDisable() => _dialoge.OnDialogeEnd -= OnDiologeEnd;

    private void OnDiologeEnd()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
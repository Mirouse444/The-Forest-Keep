using UnityEngine;

public class OnFinalStartDiologEnd : MonoBehaviour
{
    [SerializeField] private TextDialogeButton _dialoge;

    private void OnEnable() => _dialoge.OnDialogeEnd += OnDialogeEnd;
    private void OnDisable() => _dialoge.OnDialogeEnd -= OnDialogeEnd;

    private void OnDialogeEnd()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
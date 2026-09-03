using UnityEngine;

public class ClosePanelAfterDialog : MonoBehaviour
{
    [SerializeField] private TextDialogeButton _startDialog;
    [SerializeField] private TextDialogeButton _edgeDialog;
    
    private void OnEnable()
    {
        _startDialog.OnDialogeEnd += OnDialogEnd;
        _edgeDialog.OnDialogeEnd += OnDialogEnd;
    }

    private void OnDisable()
    {
        _startDialog.OnDialogeEnd -= OnDialogEnd;
        _edgeDialog.OnDialogeEnd -= OnDialogEnd;
    }

    private void OnDialogEnd()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
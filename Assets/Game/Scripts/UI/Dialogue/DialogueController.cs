using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button _nextButton;
    [SerializeField] private DialogueTyper _typer;
    [SerializeField] private GameObject _dialoguePanel;

    [Header("Settings")]
    [SerializeField] private string[] _messages;
    [SerializeField] private bool _pauseTimeOnStart = true;

    [Header("Events")]
    public UnityEvent OnDialogueStart;
    public UnityEvent OnDialogueEnd;

    private int _currentMessageIndex;

    private void OnEnable() => _nextButton.onClick.AddListener(OnNextButtonClicked);
    private void OnDisable() => _nextButton.onClick.RemoveListener(OnNextButtonClicked);

    public void StartDialogue()
    {
        if (_messages == null || _messages.Length == 0) return;

        if (_pauseTimeOnStart)
            Time.timeScale = 0f;

        _currentMessageIndex = 0;
        _dialoguePanel.SetActive(true);
        gameObject.SetActive(true);

        OnDialogueStart?.Invoke();
        ShowNextMessage();
    }

    private void OnNextButtonClicked()
    {
        if (_typer.IsTyping)
        {
            _typer.SkipTyping();
            return;
        }

        if (_currentMessageIndex < _messages.Length)
        {
            ShowNextMessage();
        }
        else
        {
            EndDialogue();
        }
    }

    private void ShowNextMessage()
    {
        _typer.ShowDialogue(_messages[_currentMessageIndex]);
        _currentMessageIndex++;
    }

    private void EndDialogue()
    {
        if (_pauseTimeOnStart)
            Time.timeScale = 1f;

        _dialoguePanel.SetActive(false);
        gameObject.SetActive(false);

        OnDialogueEnd?.Invoke();
    }
}
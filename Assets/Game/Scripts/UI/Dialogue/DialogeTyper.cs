using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueTyper : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private float _typingSpeed = 0.03f;

    private Coroutine _typingCoroutine;
    private WaitForSecondsRealtime _typingWait;

    public bool IsTyping { get; private set; }

    private void Awake() => _typingWait = new WaitForSecondsRealtime(_typingSpeed);

    public void ShowDialogue(string message)
    {
        if (_typingCoroutine != null) 
            StopCoroutine(_typingCoroutine);

        _typingCoroutine = StartCoroutine(TypeDialogueRoutine(message));
    }

    private IEnumerator TypeDialogueRoutine(string message)
    {
        IsTyping = true;
        _dialogueText.text = message;
        _dialogueText.maxVisibleCharacters = 0;

        for (int i = 0; i <= message.Length; i++)
        {
            _dialogueText.maxVisibleCharacters = i;
            yield return _typingWait;
        }

        IsTyping = false;
        _typingCoroutine = null;
    }

    public void SkipTyping()
    {
        if (_typingCoroutine != null)
        {
            StopCoroutine(_typingCoroutine);
            _typingCoroutine = null;
        }

        IsTyping = false;
        _dialogueText.maxVisibleCharacters = _dialogueText.text.Length;
    }
}
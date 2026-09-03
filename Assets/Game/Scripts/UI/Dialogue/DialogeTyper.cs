using System.Collections;
using UnityEngine;
using TMPro;            

public class DialogeTyper : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dialogText;
    [SerializeField] private float _typingSpeed = 0.03f;

    private Coroutine _typingCoroutine;
    private WaitForSecondsRealtime _typingWait;

    private void Awake() => _typingWait = new WaitForSecondsRealtime(_typingSpeed);

    public bool _isTyping {private set; get; }

    public void ShowDialog(string message)
    {
        if (_typingCoroutine != null) 
            StopCoroutine(_typingCoroutine);

        _typingCoroutine = StartCoroutine(TypeDialogRoutine(message));
    }

    private IEnumerator TypeDialogRoutine(string message)
    {
        _isTyping = true;
        _dialogText.text = message;
        _dialogText.maxVisibleCharacters = 0;
        
        int totalCharacters = message.Length;

        for (int i = 0; i <= totalCharacters; i++)
        {
            _dialogText.maxVisibleCharacters = i;
            yield return _typingWait;
        }

        _isTyping = false;
        _typingCoroutine = null;
    }
    
    public void SkipTyping()
    {
        if (_typingCoroutine != null) 
        {
            StopCoroutine(_typingCoroutine);
            _typingCoroutine = null;
        }
        
        _isTyping = false;
        _dialogText.maxVisibleCharacters = _dialogText.text.Length;
    }
}
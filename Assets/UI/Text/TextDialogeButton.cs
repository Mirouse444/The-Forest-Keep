using System;
using UnityEngine;
using UnityEngine.UI;

public class TextDialogeButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private string[] _messages;
    [SerializeField] private DialogeTyper _text;
    
    private int _currentMessageIndex;
    
    public event Action OnDialogeEnd;
    
    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
        if (_messages.Length <= 0) return;
        
        _currentMessageIndex = 0;
        _text.ShowDialog(_messages[_currentMessageIndex++]);
    }

    private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

    private void OnButtonClick()
    {
        if (_text._isTyping)
        {
            _text.SkipTyping();
            return;
        }

        if (_currentMessageIndex < _messages.Length)
        {
            _text.ShowDialog(_messages[_currentMessageIndex++]);
        }
        else
        {
            OnDialogeEnd?.Invoke();
            _currentMessageIndex = 0;
        }
    }
}
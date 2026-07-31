using System;
using UnityEngine;

public class UIInputDetector : MonoBehaviour
{
    private PlayerInput _input;
    public event Action CallMenu;

    private void Awake()
    {
        _input = new PlayerInput();

        _input.UI.CallMemu.performed += ctx => CallMenu?.Invoke();
    }

    private void OnEnable() => _input.UI.Enable();

    private void OnDisable() => _input.Disable();
}

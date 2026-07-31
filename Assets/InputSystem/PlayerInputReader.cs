using System;
using UnityEngine;

public class PlayerInputReader : MonoBehaviour
{
    private PlayerInput _input;

    public event Action OnFireStarted;
    public event Action OnFireCanceled;

    private void Awake()
    {
        _input = new PlayerInput();

        _input.Player.Fire.started += _ => OnFireStarted?.Invoke();
        _input.Player.Fire.canceled += _ => OnFireCanceled?.Invoke();
    }

    private void OnEnable() => _input.Enable();
    private void OnDisable() => _input.Disable();

    public Vector2 MouseScreenPosition => _input.Player.Aim.ReadValue<Vector2>();
}

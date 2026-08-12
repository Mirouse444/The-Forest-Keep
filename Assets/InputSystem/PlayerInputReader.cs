using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    private PlayerInput _input;

    public event Action OnFireStarted;
    public event Action OnFireCanceled;

    private void Awake() => _input = new PlayerInput();

    private void OnEnable()
    {
        _input.Enable();

        _input.Player.Fire.started += StartFire;
        _input.Player.Fire.canceled += CancelFire;
    }

    private void OnDisable()
    {
        _input.Disable();
        
        _input.Player.Fire.started -= StartFire;
        _input.Player.Fire.canceled -= CancelFire;
    }

    public Vector2 MouseScreenPosition => _input.Player.Aim.ReadValue<Vector2>();

    private void StartFire(InputAction.CallbackContext ctx) => OnFireStarted?.Invoke();
    private void CancelFire(InputAction.CallbackContext ctx) => OnFireCanceled?.Invoke();
}

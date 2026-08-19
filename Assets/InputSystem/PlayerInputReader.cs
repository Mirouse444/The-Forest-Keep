using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    private PlayerInput _input;

    public event Action OnFireStarted;
    public event Action OnFireCanceled;
    public event Action OnReloadStarted;
    
    private void Awake()
    {
        _input = new PlayerInput();
    }

    private void OnEnable()
    {
        _input.Enable();
        
        _input.Player.Fire.started += StartFire;
        _input.Player.Fire.canceled += CancelFire;
        _input.Player.Reload.started += StartReload;
    }

    private void OnDisable()
    {
        _input.Disable();
        
        _input.Player.Fire.started -= StartFire;
        _input.Player.Fire.canceled -= CancelFire;
        _input.Player.Reload.started -= StartReload;
    }

    public Vector2 MouseScreenPosition => _input.Player.Aim.ReadValue<Vector2>();

    private void StartFire(InputAction.CallbackContext ctx) => OnFireStarted?.Invoke();
    private void CancelFire(InputAction.CallbackContext ctx) => OnFireCanceled?.Invoke();
    private void StartReload(InputAction.CallbackContext ctx) => OnReloadStarted?.Invoke();
}

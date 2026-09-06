using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    PlayerControls _playerInput;
    
    public event Action OnButton1;
    public event Action OnButton2;
    public event Action OnButton3;
    public event Action OnButton4;
    public event Action OnTeleport;
    
    public float MoveHorizontal => _playerInput.Player.Move.ReadValue<Vector2>().x;
    public float MoveVertical => _playerInput.Player.Move.ReadValue<Vector2>().y;
    public bool JumpPressed => _playerInput.Player.Jump.WasPressedThisFrame(); 
    public bool JumpReleased => _playerInput.Player.Jump.WasReleasedThisFrame();
    
    private void Awake() => _playerInput = new PlayerControls();

    private void OnEnable()
    {
        _playerInput.Enable();
        
        _playerInput.Player.Slot1.performed += OnPressingButton1;
        _playerInput.Player.Slot2.performed += OnPressingButton2;
        _playerInput.Player.Slot3.performed += OnPressingButton3;
        _playerInput.Player.Slot4.performed += OnPressingButton4;
        _playerInput.Player.Teleport.performed += OnPressingButtonT;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        
        _playerInput.Player.Slot1.performed -= OnPressingButton1;
        _playerInput.Player.Slot2.performed -= OnPressingButton2;
        _playerInput.Player.Slot3.performed -= OnPressingButton3;
        _playerInput.Player.Slot4.performed -= OnPressingButton4;
        _playerInput.Player.Teleport.performed -= OnPressingButtonT;
    }
    
    private void OnPressingButton1(InputAction.CallbackContext context) => OnButton1?.Invoke();
    private void OnPressingButton2(InputAction.CallbackContext context) => OnButton2?.Invoke();
    private void OnPressingButton3(InputAction.CallbackContext context) => OnButton3?.Invoke();
    private void OnPressingButton4(InputAction.CallbackContext context) => OnButton4?.Invoke();
    private void OnPressingButtonT(InputAction.CallbackContext context) => OnTeleport?.Invoke();
}
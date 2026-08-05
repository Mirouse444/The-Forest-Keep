using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    PlayerInput _playerInput;

    public float MoveHorizontal => _playerInput.Player.Move.ReadValue<Vector2>().x;
    public float MoveVertical => _playerInput.Player.Move.ReadValue<Vector2>().y;
    public bool JumpPressed => _playerInput.Player.Jump.WasPressedThisFrame(); 
    public bool JumpReleased => _playerInput.Player.Jump.WasReleasedThisFrame();
    
    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
}
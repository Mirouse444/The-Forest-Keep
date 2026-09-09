using UnityEngine;

[RequireComponent(typeof(PlayerInputReader))]
public class PlayerAim : MonoBehaviour
{
    private Camera _mainCamera;
    private PlayerInputReader _inputReader;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _inputReader = GetComponent<PlayerInputReader>();
    }

    public Vector2 GetAimDirection(Vector2 originPoint)
    {
        Vector2 mouseScreenPosition = _inputReader.MouseScreenPosition;
        Vector2 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        return (mouseWorldPosition - originPoint).normalized;
    }
}
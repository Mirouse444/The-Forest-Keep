using UnityEngine;
using UnityEngine.UI;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _teleportPosition;
    [SerializeField] private Button _teleportButton;
    [SerializeField] private PlayerInputController _playerInput;

    private void OnEnable()
    {
        _teleportButton.onClick.AddListener(Teleport);
        _playerInput.OnTeleport += Teleport;
    }

    private void OnDisable()
    {
        _teleportButton.onClick.RemoveListener(Teleport);
        _playerInput.OnTeleport -= Teleport;
    }

    private void Teleport() => _player.transform.position = _teleportPosition.position;
}
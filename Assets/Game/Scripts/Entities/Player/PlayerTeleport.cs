using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _teleportPosition;
    [SerializeField] private Button _teleportButton;
    [SerializeField] private Image _teleportImage;
    [SerializeField] private PlayerInputController _playerInput;
    [SerializeField, Min(0)] private float _reload;
    
    private bool _isReloading;
    
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

    private void Teleport()
    {
        if (_isReloading) return;
        
        _player.transform.position = _teleportPosition.position;
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        _isReloading = true;
        float timer = 0f;

        while (timer < _reload)
        {
            _teleportImage.fillAmount =  timer / _reload;
            timer += Time.deltaTime;
            yield return null;
        }
        
        _teleportImage.fillAmount = 1f;
        _isReloading = false;
    }
}
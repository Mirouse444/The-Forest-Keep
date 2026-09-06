using UnityEngine;
using System.Collections;
using System;

public class PlayerRespawner : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private HealthComponent _playerHealth;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private int _timeToSpawn;

    private WaitForSeconds _spawnTime;

    public event Action<int> OnRespawnStarted;

    private void Awake() => _spawnTime = new WaitForSeconds(_timeToSpawn);

    private void OnEnable() => _playerHealth.State.OnDeath += HandleDeath;
    private void OnDisable() => _playerHealth.State.OnDeath -= HandleDeath;

    private void HandleDeath() => StartCoroutine(RespawnCoroutine());

    private IEnumerator RespawnCoroutine()
    {
        _player.SetActive(false);
        OnRespawnStarted?.Invoke(_timeToSpawn);

        yield return _spawnTime;

        _player.transform.position = _spawnPosition.position;
        _player.SetActive(true);

        _playerHealth.ResetToMax();
    }
}
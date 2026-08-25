using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
internal class Tower : MonoBehaviour, ITarget
{
    [SerializeField] private GameObject _gameOverPanel;
    
    private HealthComponent _health;

    private void Awake() => _health = GetComponent<HealthComponent>();

    public Vector3 Position => transform.position;


    private void OnEnable() => _health.State.OnDeath += GameOver;
    private void OnDisable() => _health.State.OnDeath -= GameOver;

    private void GameOver()
    {
        _gameOverPanel.SetActive(true);
    }
}

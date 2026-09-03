using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private HealthComponent _towerHealth;

    private void OnEnable() => _towerHealth.State.OnDeath += Show;
    private void OnDisable() => _towerHealth.State.OnDeath -= Show;

    private void Show()
    {
        _panel.SetActive(true);
    }

    public void RestartGame()
    {
        _panel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
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
        Time.timeScale = 0f;

        _panel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        _panel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
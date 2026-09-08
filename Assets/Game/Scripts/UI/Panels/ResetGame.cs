using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void LoadSceneByIndex(int index) => SceneManager.LoadScene(index);
    
}
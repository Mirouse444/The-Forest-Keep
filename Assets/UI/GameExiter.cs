using UnityEngine;

public class GameExiter : MonoBehaviour
{
    public void SaveAndQuit()
    {
        SaveGame();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }

    private void SaveGame()
    { 
        PlayerPrefs.Save();
    }
}
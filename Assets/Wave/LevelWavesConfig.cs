using UnityEngine;

[CreateAssetMenu(fileName = "LevelWavesConfig", menuName = "Scriptable Objects/LevelWavesConfig")]

public class LevelWavesConfig : ScriptableObject
{
    [SerializeField] private NightConfig[] _nightConfigArray;
}
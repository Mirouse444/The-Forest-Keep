using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NightConfig", menuName = "Scriptable Objects/LevelWavesConfig/NightConfig")]
public class NightConfig : ScriptableObject
{
    [System.Serializable]
    public struct WaveGroup
    {
        [SerializeField] private EnemyCore _enemyPrefab;
        [SerializeField] private int _count;
        [SerializeField] private float _spawnInterval;

        public EnemyCore EnemyPrefab => _enemyPrefab;
        public int Count => _count;
        public float SpawnInterval => _spawnInterval;
    }

    [SerializeField] private int _nightNumber;
    [SerializeField] private List<WaveGroup> _waves;

    public int NightNumber => _nightNumber;
    public IReadOnlyList<WaveGroup> Waves => _waves;
}
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NightConfig", menuName = "Wave/NightConfig")]
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
    
    [SerializeField] private List<WaveGroup> _waves;
    
    public IReadOnlyList<WaveGroup> Waves => _waves;
}
using System.Collections;
using UnityEngine;

 [RequireComponent(typeof(IEnemyFactory))]
 public class WaveDirector : MonoBehaviour
 {
     [SerializeField] private SpawnerWavesConfig _leftSpawnerConfig;
     [SerializeField] private EnemySpawner _leftSpawner; 
     [SerializeField] private SpawnerWavesConfig _rightSpawnerConfig;
     [SerializeField] private EnemySpawner  _rightSpawner;
     [SerializeField] private float _timeRange;
     
     private IEnemyFactory _factory;
     private WaitForSeconds _rangeTime;
     
     public event System.Action<NightConfig> OnWaveSpawn;
     public event System.Action<EnemyCore> OnEnemySpawn;
     
     private void Awake()
     {
         _factory = GetComponent<IEnemyFactory>();
         _rangeTime = new WaitForSeconds(_timeRange);
     }

     private void OnEnable()
     {
         _leftSpawnerConfig.OnEnemyGo += LeftSpawnerActivate;
         _rightSpawnerConfig.OnEnemyGo += RightSpawnerActivate;
     }

     private void OnDisable()
     {
         _leftSpawnerConfig.OnEnemyGo  -= LeftSpawnerActivate;
         _rightSpawnerConfig.OnEnemyGo -= RightSpawnerActivate;
     }
     
     private void LeftSpawnerActivate(NightConfig config) => StartCoroutine(SpawnCoroutine(config, _leftSpawner));

     private void RightSpawnerActivate(NightConfig config) => StartCoroutine(SpawnCoroutine(config, _rightSpawner));

     private IEnumerator SpawnCoroutine(NightConfig config, EnemySpawner  spawner)
     {
        OnWaveSpawn?.Invoke(config);
         
         foreach (var group in config.Waves)
         {
             if(group.Count != 0)
             {
                 EnemyCore enemy = _factory.GetEnemy(group.EnemyPrefab);
                 SetEnemy(spawner, enemy);

                 for (int i = 1; i < group.Count; i++)
                 {
                     yield return _rangeTime;

                     enemy = _factory.GetEnemy(group.EnemyPrefab);
                     SetEnemy(spawner, enemy);
                 }

                 yield return new WaitForSeconds(group.SpawnInterval);
             }
         }
     }

     private void SetEnemy(EnemySpawner spawner, EnemyCore enemy)
     {
         OnEnemySpawn?.Invoke(enemy);
         spawner.Spawn(enemy);
     }
 }
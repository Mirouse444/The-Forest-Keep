using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

 [RequireComponent(typeof(IEnemyFactory))]
 
 public class WaveDirector : MonoBehaviour
 {
     [SerializeField] private SpawnerWavesConfig _leftSpawnerConfig;
     [SerializeField] private EnemySpawner _leftSpawner; 
     [SerializeField] private SpawnerWavesConfig _rightSpawnerConfig;
     [SerializeField] private EnemySpawner  _rightSpawner;
     [SerializeField] private float _timeRange;
     
     private IEnemyFactory _factory;
     private CancellationTokenSource _cts;
     
     public event System.Action<NightConfig> OnWaveSpawn;
     public event System.Action<EnemyCore> OnEnemySpawn;
     
     private void Awake() => _factory = GetComponent<IEnemyFactory>();

     private void OnEnable()
     {
         _cts = new CancellationTokenSource();
         
         _leftSpawnerConfig.OnEnemyGo += LeftSpawnerActivate;
         _rightSpawnerConfig.OnEnemyGo += RightSpawnerActivate;
     }

     private void OnDisable()
     {
         _cts.Cancel();
         _cts.Dispose();

         _leftSpawnerConfig.OnEnemyGo -= LeftSpawnerActivate;
         _rightSpawnerConfig.OnEnemyGo -= RightSpawnerActivate;
     }
     
     private void LeftSpawnerActivate(NightConfig config) => SpawnAsync(config, _leftSpawner, _cts.Token).Forget();

     private void RightSpawnerActivate(NightConfig config) => SpawnAsync(config, _rightSpawner, _cts.Token).Forget();

     private async UniTaskVoid SpawnAsync(NightConfig config, EnemySpawner spawner, CancellationToken token)
     {
         int rangeTimeMs = (int)(_timeRange * 1000);
         try
         {
             OnWaveSpawn?.Invoke(config);
            
             foreach (var group in config.Waves)
             {
                 if (group.Count != 0)
                 {
                     EnemyCore enemy = _factory.GetEnemy(group.EnemyPrefab);
                     SetEnemy(spawner, enemy);

                     for (int i = 1; i < group.Count; i++)
                     {
                         await UniTask.Delay(rangeTimeMs, cancellationToken: token);

                         enemy = _factory.GetEnemy(group.EnemyPrefab);
                         SetEnemy(spawner, enemy);
                     }

                     int spawnIntervalMs = (int)(group.SpawnInterval * 1000);
                     await UniTask.Delay(spawnIntervalMs, cancellationToken: token);
                 }
             }
         }
         catch (System.OperationCanceledException) { }
     }

     private void SetEnemy(EnemySpawner spawner, EnemyCore enemy)
     {
         OnEnemySpawn?.Invoke(enemy);
         spawner.Spawn(enemy);
     }
 }
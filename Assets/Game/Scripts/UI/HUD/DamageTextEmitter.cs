using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class DamageTextEmitter : MonoBehaviour
{
    [SerializeField] private HitEffectsSO _effectsSO;
    [SerializeField] private HealthComponent _health;
    
    private IDamageTextSpawner _textSpawner;

    private void Start() => _textSpawner = _effectsSO.Spawner;

    private void OnEnable() => _health.OnDamageProcessed += SpawnDamageText;

    private void OnDisable() => _health.OnDamageProcessed -= SpawnDamageText;

    private void SpawnDamageText(DamageResult result) => 
        _textSpawner.SpawnText(result.HitPosition, result.FinalDamage, result.IsCritical);
}
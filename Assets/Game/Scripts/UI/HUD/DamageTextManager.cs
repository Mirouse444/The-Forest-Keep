using UnityEngine;
using UnityEngine.Pool;

public class DamageTextManager : MonoBehaviour, IDamageTextSpawner
{
    [SerializeField] private HitEffectsSO  _hitEffects;
    [SerializeField] private DamagePopup _baseTextPrefab;
    [SerializeField] private DamagePopup _critTextPrefab;

    private ObjectPool<DamagePopup> _basePool;
    private ObjectPool<DamagePopup> _critPool;

    private void Awake()
    {
        _hitEffects.Spawner = this; 
        
        _basePool = CreatePool(_baseTextPrefab);
        _critPool = CreatePool(_critTextPrefab);
    }

    public void SpawnText(Vector3 position, int damage, bool isCrit)
    {
        var pool = isCrit ? _critPool : _basePool;
        var popup = pool.Get();
        
        popup.transform.position = position + Vector3.up * 0.5f;
        popup.Setup(damage, pool.Release);
    }

    private ObjectPool<DamagePopup> CreatePool(DamagePopup prefab) =>
        new(
            createFunc: () => Instantiate(prefab, Vector3.zero, Quaternion.identity),
            actionOnGet: popup => popup.gameObject.SetActive(true),
            actionOnRelease: popup => popup.gameObject.SetActive(false),
            actionOnDestroy: popup => Destroy(popup.gameObject),
            defaultCapacity: 20, maxSize: 40
           );
}
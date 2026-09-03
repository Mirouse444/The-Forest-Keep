using UnityEngine;
using UnityEngine.Pool;

public class ExplosionEffectPool : MonoBehaviour
{
    [SerializeField] private ExplosionEffect _prefab;
    [SerializeField] private ExplosionEffectPoolSO _scriptableObject;

    public ExplosionEffect GetEffect()
    {
        ExplosionEffect effect = _pool.Get();
        effect.Release = _pool.Release;
        return effect;
    }

    private ObjectPool<ExplosionEffect> _pool;
    
    private void Awake()
    {
        _scriptableObject.PoolSetter = this;
        
        _pool = new ObjectPool<ExplosionEffect>
        (
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (ExplosionEffect effect) => { effect.gameObject.SetActive(true); } ,
            actionOnRelease: (ExplosionEffect effect) => { effect.gameObject.SetActive(false); },
            actionOnDestroy: (ExplosionEffect effect) => { Destroy(effect.gameObject); },
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 20
        );
    }
}
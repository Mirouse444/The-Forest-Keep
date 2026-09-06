using UnityEngine;
using UnityEngine.Pool;

public class ExplosionAudioPool : MonoBehaviour
{
    [SerializeField] private ExplosionAudio _prefab;
    [SerializeField] private ExplosionAudioPoolSO _explosionAudioPoolSo;
    
    private ObjectPool<ExplosionAudio> _pool;

    private void Awake()
    {
        _explosionAudioPoolSo.Pool = this;
        
        _pool = new ObjectPool<ExplosionAudio>(
            createFunc: CreateExplosionAudio,
            actionOnGet: OnTakeExplosionAudioFromPool,
            actionOnRelease: OnReturnExplosionAudioToPool,
            actionOnDestroy: OnDestroyExplosionAudio,
            collectionCheck: false,
            defaultCapacity: 30,
            maxSize: 50
        );
    }

    public void Spawn(Vector3 position) => _pool.Get().transform.position = position;

    private ExplosionAudio CreateExplosionAudio()
    {
        ExplosionAudio audio = Instantiate(_prefab, Vector3.zero, Quaternion.identity);
        audio.ReleaseAction = _pool.Release;

        return audio;
    }
    
    private void OnTakeExplosionAudioFromPool(ExplosionAudio explosionAudio) => explosionAudio.gameObject.SetActive(true);
    private void OnReturnExplosionAudioToPool(ExplosionAudio explosionAudio) => explosionAudio.gameObject.SetActive(false);
    private void OnDestroyExplosionAudio(ExplosionAudio  explosionAudio) => Destroy(explosionAudio.gameObject);
}
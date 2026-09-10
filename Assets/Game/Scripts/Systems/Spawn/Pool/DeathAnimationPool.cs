using System.Collections;
using UnityEngine.Pool;
using UnityEngine;

public class DeathAnimationPool : MonoBehaviour, IAnimationSpawner
{
    [SerializeField] private DeathAnimationSO _animationSo;
    [SerializeField] private Animator _modelPrefab;
    [SerializeField, Min(0)] private float _deathTime = 0.5f;
    
    private ObjectPool<Animator> _pool;
    private WaitForSeconds _deathWait;

    private void Awake()
    {
        _animationSo.DeathAnimation = this;
        _deathWait = new WaitForSeconds(_deathTime);
        
        _pool = new ObjectPool<Animator>
        (
            createFunc: () => Instantiate(_modelPrefab),
            actionOnGet: (Animator model) => model.gameObject.SetActive(true),
            actionOnRelease: (Animator model) => model.gameObject.SetActive(false),
            actionOnDestroy: (Animator model) => Destroy(model.gameObject),
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 17
        );
    }

    public void SpawnDeathModel(Vector3 position, Quaternion rotation)
    {
        Animator corpseModel = _pool.Get();
        corpseModel.transform.SetPositionAndRotation(position, rotation);
        
        corpseModel.Rebind();
        corpseModel.ResetTrigger("Death");
        corpseModel.SetFloat("RandomValue", Random.value);
        corpseModel.SetTrigger("Death");
        
        corpseModel.Update(0f);
        
        StartCoroutine(DeathAnimationCoroutine(corpseModel));
    }

    private IEnumerator DeathAnimationCoroutine(Animator corpseModel)
    {
        yield return _deathWait;
        _pool.Release(corpseModel);
    }
}
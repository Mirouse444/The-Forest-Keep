using System.Collections;
using UnityEngine.Pool;
using UnityEngine;

public class DeathAnimationPool : MonoBehaviour, IAnimationSpawner
{
    [SerializeField] private DeathAnimationSO _animationSo;
    [SerializeField] private Animator _modelPrefab;
    [SerializeField] private float _deathTime = 0.5f;
    
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
        corpseModel.transform.SetLocalPositionAndRotation(position, rotation);

        StartCoroutine(DeathAnimationCoroutine(corpseModel));
    }

    private IEnumerator DeathAnimationCoroutine(Animator corpseModel)
    {
        string clipName = Random.value > 0.5f ? "death_01" : "death_02"; 
        corpseModel.Play(clipName, -1, 0f);

        yield return _deathWait;

        _pool.Release(corpseModel);
    }
}
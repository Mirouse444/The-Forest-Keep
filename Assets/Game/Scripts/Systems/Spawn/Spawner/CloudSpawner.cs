using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] private CloudPool _pool;
    [SerializeField] private float _spawnInterval = 3f;

    [SerializeField] private float _spawnXPosition;

    [SerializeField] private float _minSpawnY;
    [SerializeField] private float _maxSpawnY;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            _timer = 0f;
            SpawnCloud();
        }
    }

    private void SpawnCloud()
    {
        Cloud cloud = _pool.GetRandomCloud();

        if (cloud == null) return;

        float spawnPointX = _spawnXPosition - cloud.HalfWidth;

        float spawnPointY = Random.Range(_minSpawnY, _maxSpawnY);

        cloud.transform.position = new Vector3(spawnPointX, spawnPointY, 0f);
        cloud.gameObject.SetActive(true);
    }
}
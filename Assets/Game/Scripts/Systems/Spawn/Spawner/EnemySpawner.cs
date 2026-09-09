using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPosition;

    public void Spawn(EnemyCore enemy)
    {
        enemy.transform.SetPositionAndRotation(_spawnPosition.position, Quaternion.identity);
    }
}
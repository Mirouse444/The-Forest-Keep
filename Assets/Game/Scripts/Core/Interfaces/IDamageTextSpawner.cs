using UnityEngine;

public interface IDamageTextSpawner
{
    void SpawnText(Vector3 position, int damage, bool isCrit);
}
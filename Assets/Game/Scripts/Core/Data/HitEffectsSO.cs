using UnityEngine;

[CreateAssetMenu(menuName = "Pool/Text/Hit Effects", fileName = "Hit Effects", order = 0)]
public class HitEffectsSO : ScriptableObject
{
    public IDamageTextSpawner Spawner {get; set; }
}
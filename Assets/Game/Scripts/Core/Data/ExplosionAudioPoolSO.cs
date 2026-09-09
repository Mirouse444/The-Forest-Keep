using UnityEngine;

[CreateAssetMenu(menuName = "Pool/Audio/ExplosionAudio", fileName = "AudioPoolSO", order = 0)]
public class ExplosionAudioPoolSO: ScriptableObject
{
    public ExplosionAudioPool Pool { set; private get; }
    public void Spawn(Vector3 positon) => Pool.Spawn(positon);
}
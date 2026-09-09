using UnityEngine;

[CreateAssetMenu(menuName = "Pool/Animation/Death", fileName = "Death", order = 0)]
public class DeathAnimationSO : ScriptableObject
{
    public IAnimationSpawner DeathAnimation {get; set; }
}
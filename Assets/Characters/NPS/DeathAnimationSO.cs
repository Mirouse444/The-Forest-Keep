using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Animation/Death", fileName = "Death", order = 0)]
public class DeathAnimationSO : ScriptableObject
{
    public IAnimationSpawner DeathAnimation {get; set; }
}
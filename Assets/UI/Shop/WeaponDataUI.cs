using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Game/Weapon Data")]
public class WeaponDataUI : ScriptableObject
{
    public WeaponCategory Category;
    public GameObject WeaponPrefab;
    public Sprite UIIcon;
}
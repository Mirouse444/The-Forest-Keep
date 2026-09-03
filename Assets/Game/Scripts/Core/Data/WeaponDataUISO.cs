using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon/Data")]
public class WeaponDataUISO : ScriptableObject
{
    public WeaponCategory Category;
    public GameObject WeaponPrefab;
    public Sprite UIIcon;
}
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapon/Data")]
public class WeaponDataUISO : ScriptableObject
{
    [SerializeField] private WeaponCategory _category;
    [SerializeField] private GameObject _weaponPrefab;
    [SerializeField] private Sprite _uiIcon;
    
    public WeaponCategory Category => _category;
    public GameObject WeaponPrefab => _weaponPrefab;
    public Sprite UIIcon => _uiIcon;
}
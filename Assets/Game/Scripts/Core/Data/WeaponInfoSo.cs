using UnityEngine;


[CreateAssetMenu(menuName = "Weapon/Info", fileName = "WeaponInfoSo", order = 0)]
public class WeaponInfoSo : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField, Min(0)] private int _minDamage;
    [SerializeField, Min(0)] private int _maxDamage;
    [SerializeField, Min(0)] private int _criticalDamage;
    [SerializeField, Min(0)] private int _criticalChance;
    [SerializeField, Min(0)] private float _knockbackForce;
    [SerializeField, Min(0)] private float _shootSpeed;
    [SerializeField, Min(0)] private float _fireRate;
    [SerializeField, Min(0)] private int _projectileCount;
    [SerializeField, Min(0)] private int _pierceCount;
    [SerializeField, Min(0)] private float _spreadAngle;
    [SerializeField, Min(0)] private float _maxAmmo;
    [SerializeField, Min(0)] private float _reloadTime;
    
    public Sprite Icon => _icon;
    public int MinDamage => _minDamage;
    public int MaxDamage => _maxDamage;
    public int CriticalDamage => _criticalDamage;
    public int CriticalChance => _criticalChance;
    public float KnockbackForce => _knockbackForce;
    public  float ShootSpeed => _shootSpeed; 
    public float FireRate => _fireRate;
    public int ProjectileCount => _projectileCount;
    public int PierceCount => _pierceCount;
    public  float SpreadAngle => _spreadAngle;
    public  float MaxAmmo => _maxAmmo;
    public  float ReloadTime => _reloadTime;
}
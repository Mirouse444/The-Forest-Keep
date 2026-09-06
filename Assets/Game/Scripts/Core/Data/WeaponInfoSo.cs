using UnityEngine;


[CreateAssetMenu(menuName = "Weapon/Info", fileName = "WeaponInfoSo", order = 0)]
public class WeaponInfoSo : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _minDamage;
    [SerializeField] private int _maxDamage;
    [SerializeField] private int _criticalDamage;
    [SerializeField] private int _criticalChance;
    [SerializeField] private float _knockbackForce;
    [SerializeField] private float _shootSpeed;
    [SerializeField] private float _fireRate;
    [SerializeField] private int _projectileCount;
    [SerializeField] private int _pierceCount;
    [SerializeField] private float _spreadAngle;
    [SerializeField] private float _maxAmmo;
    [SerializeField] private float _reloadTime;
    
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
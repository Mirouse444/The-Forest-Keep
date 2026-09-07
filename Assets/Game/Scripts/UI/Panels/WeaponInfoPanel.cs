using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoPanel : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image _iconImage;
    
    [Header("Max Stats")]
    [SerializeField] private int _minDamage = 55;
    [SerializeField] private int _maxDamage = 65;
    [SerializeField] private int _criticalDamage = 120;
    [SerializeField] private int _criticalChance = 25;
    [SerializeField] private float _knockbackForce = 15f;
    [SerializeField] private float _shootSpeed = 60f;
    [SerializeField] private float _fireRate = 0.12f;
    [SerializeField] private int _projectileCount = 9;
    [SerializeField] private int _pierceCount = 5;
    [SerializeField] private float _spreadAngle = 8.5f;
    [SerializeField] private float _maxAmmo = 35;
    [SerializeField] private float _reloadTime = 1.3f;
    
    [Header("Weapon Stats")]
    [SerializeField] private Image _minDamageImage;
    [SerializeField] private Image _maxDamageImage;
    [SerializeField] private Image _criticalDamageImage;
    [SerializeField] private Image _criticalChanceImage;
    [SerializeField] private Image _knockbackForceImage;
    [SerializeField] private Image _shootSpeedImage;
    [SerializeField] private Image _fireRateImage;
    [SerializeField] private Image _projectileCountImage;
    [SerializeField] private Image _pierceCountImage;
    [SerializeField] private Image _spreadAngleImage;
    [SerializeField] private Image _maxAmmoImage;
    [SerializeField] private Image _reloadTimeImage;
    
    public void SetInfo(WeaponInfoSo info)
    {
        if (info == null) return;
        
        _iconImage.sprite = info.Icon;
        
        _minDamageImage.fillAmount = (float)info.MinDamage / _minDamage;
        _maxDamageImage.fillAmount = (float)info.MaxDamage / _maxDamage;
        _criticalDamageImage.fillAmount = (float)info.CriticalDamage / _criticalDamage;
        _criticalChanceImage.fillAmount = (float)info.CriticalChance / _criticalChance;
        _knockbackForceImage.fillAmount = info.KnockbackForce / _knockbackForce;
        _shootSpeedImage.fillAmount = info.ShootSpeed / _shootSpeed;
        _fireRateImage.fillAmount = _fireRate / info.FireRate;
        _projectileCountImage.fillAmount = (float)info.ProjectileCount / _projectileCount;
        _pierceCountImage.fillAmount = (float)info.PierceCount / _pierceCount;
        _spreadAngleImage.fillAmount = info.SpreadAngle / _spreadAngle;
        _maxAmmoImage.fillAmount = info.MaxAmmo / _maxAmmo;
        _reloadTimeImage.fillAmount = _reloadTime / info.ReloadTime;
    }
}
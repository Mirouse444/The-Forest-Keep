using UnityEngine;
using TMPro;

public class WeaponInfoPanel : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private UnityEngine.UI.Image _iconImage;
    
    [Header("Stats Text")]
    [SerializeField] private TextMeshProUGUI _minDamageText;
    [SerializeField] private TextMeshProUGUI _maxDamageText;
    [SerializeField] private TextMeshProUGUI _criticalDamageText;
    [SerializeField] private TextMeshProUGUI _criticalChanceText;
    [SerializeField] private TextMeshProUGUI _knockbackForceText;
    [SerializeField] private TextMeshProUGUI _shootSpeedText;
    [SerializeField] private TextMeshProUGUI _fireRateText;
    [SerializeField] private TextMeshProUGUI _projectileCountText;
    [SerializeField] private TextMeshProUGUI _pierceCountText;
    [SerializeField] private TextMeshProUGUI _spreadAngleText;
    [SerializeField] private TextMeshProUGUI _maxAmmoText;
    [SerializeField] private TextMeshProUGUI _reloadTimeText;
    
    public void SetInfo(WeaponInfoSo info)
    {
        _iconImage.sprite = info.Icon;
        _minDamageText.SetText("{0}", info.MinDamage);
        _maxDamageText.SetText("{0}", info.MaxDamage);
        _criticalDamageText.SetText("{0}", info.CriticalDamage);
        _criticalChanceText.SetText("{0}", info.CriticalChance);
        _knockbackForceText.SetText("{0}", info.KnockbackForce);
        _shootSpeedText.SetText("{0}", info.ShootSpeed);
        _fireRateText.SetText("{0}", info.FireRate);
        _projectileCountText.SetText("{0}", info.ProjectileCount);
        _pierceCountText.SetText("{0}", info.PierceCount);
        _spreadAngleText.SetText("{0}", info.SpreadAngle);
        _maxAmmoText.SetText("{0}", info.MaxAmmo);
        _reloadTimeText.SetText("{0}", info.ReloadTime);
    }
}
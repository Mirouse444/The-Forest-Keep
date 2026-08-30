using UnityEngine;

[RequireComponent(typeof(IWeaponLauncher))]
public class ChargeWeapon : MonoBehaviour, IWeaponTrigger
{
    [SerializeField] private float _maxChargeTime = 1.5f;
    [SerializeField] private float _minChargeTime = 0.5f;

    private IWeaponLauncher _weaponLauncher;
    private float _chargeStartTime;
    private bool _isCharging;

    private void Awake()
    {
        _weaponLauncher = GetComponent<IWeaponLauncher>();
    }

    public void OnTriggerPressed()
    {
        _isCharging = true;
        _chargeStartTime = Time.time;
    }

    public void OnTriggerReleased()
    {
        if (!_isCharging) return;
        _isCharging = false;

        float chargeDuration = Time.time - _chargeStartTime;

        if (chargeDuration < _minChargeTime) return;

        float chargePercent = Mathf.Clamp01(chargeDuration / _maxChargeTime);

        Vector2 releaseDirection = _weaponLauncher.transform.right;

        _weaponLauncher.Fire(releaseDirection, chargePercent);
    }
}
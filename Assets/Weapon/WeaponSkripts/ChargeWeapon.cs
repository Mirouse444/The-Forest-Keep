using UnityEngine;

[RequireComponent(typeof(IGunLauncher))]
public class ChargeWeapon : MonoBehaviour, IWeaponTrigger
{
    [SerializeField] private float _maxChargeTime = 1.5f;
    [SerializeField] private float _minChargeTime = 0.5f;

    private IGunLauncher _launcher;
    private float _chargeStartTime;
    private bool _isCharging;

    private void Awake()
    {
        _launcher = GetComponent<IGunLauncher>();
    }

    public void OnTriggerPressed(IAimProvider aimProvider)
    {
        _isCharging = true;
        _chargeStartTime = Time.time;
    }

    public void OnTriggerReleased(IAimProvider aimProvider)
    {
        if (!_isCharging) return;
        _isCharging = false;

        float chargeDuration = Time.time - _chargeStartTime;

        if (chargeDuration < _minChargeTime) return;

        float chargePercent = Mathf.Clamp01(chargeDuration / _maxChargeTime);

        Vector2 releaseDirection = aimProvider.GetAimDirection(_launcher.transform.position);

        _launcher.Fire(releaseDirection, chargePercent);
    }
}
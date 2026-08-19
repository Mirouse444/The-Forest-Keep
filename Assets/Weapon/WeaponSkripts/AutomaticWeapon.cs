using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IWeaponLauncher))]
public class AutomaticWeapon : MonoBehaviour, IWeaponTrigger
{
    [SerializeField] private float _fireRate = 0.1f;

    private IWeaponLauncher _weaponLauncher;
    private IWeaponMagazine  _magazine;
    private WaitForSeconds _fireRateTime;
    private Coroutine _fireCoroutine;

    private void Awake()
    {
        _weaponLauncher = GetComponent<IWeaponLauncher>();
        _magazine = GetComponent<IWeaponMagazine>();
        _fireRateTime = new WaitForSeconds(_fireRate);
    }

    public void OnTriggerPressed(IAimProvider aimProvider)
    {
        if (_fireCoroutine == null)
            _fireCoroutine = StartCoroutine(FireRoutine(aimProvider));
    }

    public void OnTriggerReleased(IAimProvider aimProvider)
    {
        if (_fireCoroutine != null)
        {
            StopCoroutine(_fireCoroutine);
            _fireCoroutine = null;
        }
    }

    private IEnumerator FireRoutine(IAimProvider aimProvider)
    {
        while (true)
        {
            if (!_magazine.TryConsumeAmmo()) yield break;
            
            Vector2 dir = aimProvider.GetAimDirection(_weaponLauncher.transform.position);
            _weaponLauncher.Fire(dir);

            yield return _fireRateTime;
        }
    }

    private void OnDisable() => _fireCoroutine = null;
}
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
    private float _timer;

    private void Awake()
    {
        _weaponLauncher = GetComponent<IWeaponLauncher>();
        _magazine = GetComponent<IWeaponMagazine>();
        _fireRateTime = new WaitForSeconds(_fireRate);
    }

    public void OnTriggerPressed()
    {
        if (Time.time >= _timer + _fireRate)
            _fireCoroutine ??= StartCoroutine(FireRoutine());
    }

    public void OnTriggerReleased()
    {
        if (_fireCoroutine != null)
        {
            StopCoroutine(_fireCoroutine);
            _fireCoroutine = null;
        }
    }

    private IEnumerator FireRoutine()
    {
        while (true)
        {
            if (!_magazine.TryConsumeAmmo()) yield break;
            _timer =  Time.time;
            
            Vector2 dir = _weaponLauncher.transform.right;
            _weaponLauncher.Fire(dir);
            
            yield return _fireRateTime;
        }
    }

    private void OnDisable() => _fireCoroutine = null;
}
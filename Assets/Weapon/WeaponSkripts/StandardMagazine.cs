using System.Collections;
using UnityEngine;
using System;

public class StandardMagazine : MonoBehaviour, IWeaponMagazine, IReloadMagazine, IReloadUI
{
    [SerializeField, Min(1)] private int _maxAmmo = 30;
    [SerializeField, Min(0)] private float _reloadTime = 1.5f;

    private Coroutine _reloadCoroutine;
    private WaitForSeconds _reloadWait;

    private bool IsReloading => _reloadCoroutine != null;
    
    public int CurrentAmmo { get; private set; }
    public int MaxAmmo => _maxAmmo;
    
    public bool IsEmpty => CurrentAmmo <= 0;

    public event Action<int, int> OnAmmoChanged;
    public event Action<float> OnReloadStarted;
    public event Action OnDryFire;

    private void Awake()
    {
        CurrentAmmo = _maxAmmo;
        _reloadWait = new WaitForSeconds(_reloadTime);
    }

    private void OnDisable() => _reloadCoroutine = null;

    public bool TryConsumeAmmo()
    {
        if (IsEmpty || IsReloading) { OnDryFire?.Invoke(); return false; }

        CurrentAmmo--;
        OnAmmoChanged?.Invoke(CurrentAmmo, _maxAmmo);
        return true;
    }

    public void Reload()
    {
        if (IsReloading || CurrentAmmo == _maxAmmo) return;
        
        _reloadCoroutine = StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        OnReloadStarted?.Invoke(_reloadTime);

        yield return _reloadWait;

        CurrentAmmo = _maxAmmo;
        _reloadCoroutine = null;
        
        OnAmmoChanged?.Invoke(CurrentAmmo, _maxAmmo);
    }
}
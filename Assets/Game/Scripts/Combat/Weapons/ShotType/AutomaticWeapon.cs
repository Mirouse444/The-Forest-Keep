using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(IWeaponLauncher))]
public class AutomaticWeapon : MonoBehaviour, IWeaponTrigger
{
    [SerializeField] private float _fireRate = 0.1f;

    private IWeaponLauncher _weaponLauncher;
    private IWeaponMagazine  _magazine;
    private CancellationTokenSource _fireCts;
    private float _timer;

    private void Awake()
    {
        _weaponLauncher = GetComponent<IWeaponLauncher>();
        _magazine = GetComponent<IWeaponMagazine>();
    }

    public void OnTriggerPressed()
    {
        if (Time.time >= _timer + _fireRate)
        {
            CleanupCts();
            
            _fireCts = new CancellationTokenSource();
            FireRoutine(_fireCts.Token).Forget();
        }
    }

    public void OnTriggerReleased() => CleanupCts();

    private async UniTaskVoid FireRoutine(CancellationToken token)
    {
        int fireWaitMs = (int)(_fireRate * 1000);

        try
        {
            while (true)
            {
                if (!_magazine.TryConsumeAmmo())
                {
                    await UniTask.Yield(cancellationToken: token);
                    continue;
                }

                _timer = Time.time;

                Vector2 dir = _weaponLauncher.transform.right;
                _weaponLauncher.Fire(dir);

                await UniTask.Delay(fireWaitMs, cancellationToken: token);
            }
        }
        catch (System.OperationCanceledException) { }
    }

    private void OnDisable() => CleanupCts();
    
    private void CleanupCts()
    {
        if(_fireCts == null) return;
            
        _fireCts.Cancel();
        _fireCts.Dispose();
        _fireCts = null;
    }
}
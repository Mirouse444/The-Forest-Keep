using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IGunLauncher))]
public class AutomaticWeapon : MonoBehaviour, IWeaponTrigger
{
    [SerializeField] private float _fireRate = 0.1f;

    private IGunLauncher _launcher;
    private Coroutine _fireCoroutine;
    private WaitForSeconds _fireRateTime;

    private void Awake()
    {
        _launcher = GetComponent<IGunLauncher>();
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
            Vector2 dir = aimProvider.GetAimDirection(_launcher.transform.position);
            _launcher.Fire(dir);

            yield return _fireRateTime;
        }
    }
}
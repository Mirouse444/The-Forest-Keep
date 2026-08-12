using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IGunLauncher))]
public class BufferedMeleeWeapon : MonoBehaviour, IWeaponTrigger
{
    [SerializeField] private float _attackCooldown = 0.5f;
    [SerializeField] private float _inputBufferTime = 0.15f;

    private IGunLauncher _launcher;

    private float _lastAttackTime;
    private Coroutine _bufferCoroutine;

    private void Awake()
    {
        _launcher = GetComponent<IGunLauncher>();
        _lastAttackTime = _inputBufferTime - _attackCooldown;
    }

    public void OnTriggerPressed(IAimProvider aimProvider)
    {
        if(Time.time - _lastAttackTime >= _attackCooldown - _inputBufferTime)
            if(_bufferCoroutine == null)
                _bufferCoroutine = StartCoroutine(BufferRoutine(aimProvider));
    }

    public void OnTriggerReleased(IAimProvider aimProvider) { }

    private IEnumerator BufferRoutine(IAimProvider aimProvider)
    {
        float timeRemaining = _attackCooldown - (Time.time - _lastAttackTime);

        if (timeRemaining > 0)
            yield return new WaitForSeconds(timeRemaining);

        _lastAttackTime = Time.time;

        Vector2 direction = aimProvider.GetAimDirection(_launcher.transform.position);
        _launcher.Fire(direction);

        _bufferCoroutine = null;
    }
}
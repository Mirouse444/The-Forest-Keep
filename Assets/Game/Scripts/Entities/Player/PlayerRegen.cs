using System.Collections;
using UnityEngine;

public class PlayerRegen : MonoBehaviour
{
    [SerializeField, Min(0)] private int _regenHPToTime;
    [SerializeField, Min(0)] private float _timeToRegen;
    [SerializeField, Min(0)] private float _regenCooldown;

    private WaitForSeconds _regenTime;
    private WaitUntil _resetTimer;
    private IHealable _health;
    private float _timer;

    private void Awake()
    {
        _health = GetComponent<IHealable>();
        _regenTime = new WaitForSeconds(_timeToRegen);
        _resetTimer = new WaitUntil(() => _timer < _regenCooldown);
    }

    private void OnEnable()
    {
        _health.State.OnApplyDamage += ResetRegen;
        StartCoroutine(PlayerHPRegen());
    }

    private void OnDisable() => _health.State.OnApplyDamage -= ResetRegen;

    private void ResetRegen() => _timer = 0;

    private IEnumerator PlayerHPRegen()
    {
        _timer = 0;

        while(true)
        {
            yield return null;
            _timer += Time.deltaTime;

            if(_timer >= _regenCooldown)
            {
                do
                    yield return _regenTime;
                while (_timer >= _regenCooldown && _health.AddHP(_regenHPToTime));

                yield return _resetTimer;
            }
        }
    }
}

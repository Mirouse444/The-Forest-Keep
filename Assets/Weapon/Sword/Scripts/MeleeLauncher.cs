using System;
using UnityEngine;

public class MeleeLauncher : MonoBehaviour, IWeaponLauncher, IMeleeLauncher
{
    [Space, Header("Stats")]
    [SerializeField, Min(0)] private int _damage;
    [SerializeField, Min(0)] private int _criticalDamage;
    [SerializeField, Range(0, 100)] private int _criticalChance;
    [SerializeField, Min(0)] private float _attackTime;
    [SerializeField, Min(0)] private float _knockbackForce;

    public event Action<MeleeStrikeData> OnFire;

    public void Fire(Vector2 direction, float powerMultiplier = 1)
    {
        bool isCritical = _criticalChance > UnityEngine.Random.Range(0, 100);
        int baseDamage = isCritical ? _criticalDamage : _damage;
        float facingX = Mathf.Sign(transform.root.localScale.x);
        Vector2 pushDir = new Vector2(facingX, 0.5f);

        MeleeStrikeData strikeData = new MeleeStrikeData
        (
            direction,
            pushDir,
            Mathf.RoundToInt(baseDamage * powerMultiplier),
            _knockbackForce * powerMultiplier,
            _attackTime,
            isCritical
        );

        OnFire.Invoke(strikeData);
    }
}

using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
internal class Armor : MonoBehaviour, IDamageModifier
{
    [SerializeField] private int _damageReduction;

    private HealthComponent _health;

    private void Awake() => _health = GetComponent<HealthComponent>();

    private void OnEnable() => _health.AddModifier(this);

    private void OnDisable() => _health.RemoveModifier(this);

    public DamageModifierPriority Priority => DamageModifierPriority.Additive;

    public int ProcessDamage(int finalDamage) => finalDamage - _damageReduction;
}

using System.Collections.Generic;
using UnityEngine;

internal class HealthComponent : MonoBehaviour, IDamageable, IHealable
{
    private HealthModel _healthModel = new HealthModel();

    private List<IDamageModifier> _modifiers = new List<IDamageModifier>();

    public IHealthState State => _healthModel;

    public void AddModifier(IDamageModifier modifier)
    {
        if(!_modifiers.Contains(modifier))
        {
            _modifiers.Add(modifier);
            _modifiers.Sort((a, b) => a.Priority.CompareTo(b.Priority));
        }
    }

    public void RemoveModifier(IDamageModifier modifier) => _modifiers.Remove(modifier);

    public void InitializeHealth(int maxHealth) => _healthModel.Initialization(maxHealth);

    public bool AddHP(int hp) => hp > 0 && _healthModel.AddHP(hp);

    public void ResetToMax() => _healthModel.ResetToMax();

    public void TakeDamage(int rawDamage)
    {
        int finalDamage = rawDamage;

        for (int i = 0; i < _modifiers.Count; i++)
            finalDamage = _modifiers[i].ProcessDamage(finalDamage);

        finalDamage = Mathf.Max(1, finalDamage);

        _healthModel.ApplyDamage(finalDamage);
    }
}
internal interface IDamageModifier
{
    DamageModifierPriority Priority { get; }
    int ProcessDamage(int finalDamage);
}
using UnityEngine;

public readonly struct DamageInfo
{
    public int Amount { get; }
    public bool IsCritical { get; }

    public DamageInfo(int amount, bool isCritical)
    {
        Amount = Mathf.Max(0, amount);
        IsCritical = isCritical;
    }
}
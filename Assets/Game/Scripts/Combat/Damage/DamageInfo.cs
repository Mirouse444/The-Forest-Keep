public struct DamageInfo
{
    public readonly int Amount;
    public readonly bool IsCritical;

    public DamageInfo(int amount, bool isCritical)
    {
        Amount = amount;
        IsCritical = isCritical;
    }
}
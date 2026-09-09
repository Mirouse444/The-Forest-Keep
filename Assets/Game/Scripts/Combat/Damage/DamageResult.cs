public struct DamageResult
{
    public int FinalDamage { get; }
    public bool IsCritical { get; }
    public UnityEngine.Vector3 HitPosition { get; }

    public DamageResult(int finalDamage, bool isCritical, UnityEngine.Vector3 hitPosition)
    {
        FinalDamage = finalDamage;
        IsCritical = isCritical;
        HitPosition = hitPosition;
    }
}
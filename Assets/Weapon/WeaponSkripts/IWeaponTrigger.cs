public interface IWeaponTrigger
{
    void OnTriggerPressed(IAimProvider aimProvider);
    void OnTriggerReleased(IAimProvider aimProvider);
}
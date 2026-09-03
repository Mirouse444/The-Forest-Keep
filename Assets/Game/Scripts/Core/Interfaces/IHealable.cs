public interface IHealable
{
    IHealthState State { get; }
    bool AddHP(int hp);
}
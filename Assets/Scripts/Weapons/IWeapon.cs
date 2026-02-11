public interface IWeapon
{
    Damage DealedDamage { get; }
    void Act();
    bool CanAct();
    void SetOverallDamageModifier(float modifier);
    void SetCriticalChance(float chance);
}
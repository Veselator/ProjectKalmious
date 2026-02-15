public interface IWeapon
{
    Damage DealedDamage { get; }
    void Act();
    bool CanAct();
    void SetOverallDamageModifier(float modifier);
    void SetCooldownMultiplier(float value);
    void SetCriticalChance(float chance);
}
using UnityEngine;

[System.Serializable]
public struct Damage
{
    // Структура урона

    [Min(0f)]
    public float damageHealth; // Сколько пройдёт урона без брони или если броню сняли
    [Min(0f)]
    public float damageArmor; // Сколько пройдёт урона с бронёй
    [Min(0f)]
    public float overallDamageMultiplier;
    [Min(0f)]
    public float criticalChance;

    public Damage(float damageHealth, float damageArmor, float overallDamageMultiplier, float criticalChance)
    {
        this.damageHealth = damageHealth;
        this.damageArmor = damageArmor;
        this.overallDamageMultiplier = overallDamageMultiplier;
        this.criticalChance = criticalChance;
    }

    public static Damage operator *(Damage damage, float multiplier)
    {
        return new Damage(damage.damageHealth * multiplier, damage.damageArmor * multiplier, damage.overallDamageMultiplier, damage.criticalChance);
    }

    public float MultipliedHealthDamage => damageHealth * overallDamageMultiplier;
    public float MultipliedArmorDamage => damageHealth * overallDamageMultiplier;
}
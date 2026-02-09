public class StrengthCalculator : ICharacteristicCalculator
{
    public static float GetCharacteristic(CharacteristicsHandler characteristics)
    {
        float baseDamage = 10f;
        float bonus = characteristics.Strength * 0.5f + characteristics.Luck * 0.05f;
        return baseDamage + bonus;
    }
}

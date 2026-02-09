public class IntelligenceCalculator : ICharacteristicCalculator
{
    public static float GetCharacteristic(CharacteristicsHandler characteristics)
    {
        float baseXPMultiplier = 1f;
        float bonus = characteristics.Intelligence * 0.02f + characteristics.Luck * 0.005f;
        return baseXPMultiplier + bonus;
    }
}

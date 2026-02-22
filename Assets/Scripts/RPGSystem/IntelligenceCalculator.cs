public class IntelligenceCalculator : ICharacteristicCalculator
{
    public static float GetCharacteristic(CharacteristicsHandler characteristics)
    {
        return characteristics.Intelligence * 0.073f + characteristics.Luck * 0.025f;
    }
}

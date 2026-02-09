public class SpeedCalculator : ICharacteristicCalculator
{
    public static float GetCharacteristic(CharacteristicsHandler characteristics)
    {
        float baseSpeed = 5f;
        float bonus = characteristics.Speed * 0.1f + characteristics.Luck * 0.02f;
        return baseSpeed + bonus;
    }
}

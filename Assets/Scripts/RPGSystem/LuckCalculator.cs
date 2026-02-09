public class LuckCalculator : ICharacteristicCalculator
{
    public static float GetCharacteristic(CharacteristicsHandler characteristics)
    {
        float baseCritChance = 0.05f;
        float bonus = characteristics.Luck * 0.01f;
        return baseCritChance + bonus;
    }
}

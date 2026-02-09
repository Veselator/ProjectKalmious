public class StaminaCalculator : ICharacteristicCalculator
{
    public static float GetCharacteristic(CharacteristicsHandler characteristics)
    {
        float baseHealth = 100f;
        float bonus = characteristics.Stamina * 5f + characteristics.Luck * 0.5f;
        return baseHealth + bonus;
    }
}

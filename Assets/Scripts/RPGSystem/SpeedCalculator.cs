using System;

public class SpeedCalculator : ICharacteristicCalculator
{
    private const float baseSpeed = 1f;
    private const float speedFactor = 2.02f;
    private const float luckFactor = 0.013f;

    public static float GetCharacteristic(CharacteristicsHandler characteristics)
    {
        float bonus = (float)Math.Sqrt(characteristics.Speed) * speedFactor + characteristics.Luck * luckFactor;
        return baseSpeed + bonus;
    }
}

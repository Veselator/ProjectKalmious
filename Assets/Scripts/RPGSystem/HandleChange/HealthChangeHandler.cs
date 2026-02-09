using UnityEngine;

public class HealthChangeHandler : BaseChangeHandler
{
    [SerializeField] private Health _linkedHealth;

    protected override CharacteristicType _targetCharacteristic => CharacteristicType.Stamina;

    protected override void Start()
    {
        base.Start();

        _linkedHealth.MaximumHealth = _characteristics.CalculatedStamina;
        _linkedHealth.ResetHealth();
    }

    protected override void DoChange()
    {
        _linkedHealth.MaximumHealth = _characteristics.CalculatedStamina;
    }
}

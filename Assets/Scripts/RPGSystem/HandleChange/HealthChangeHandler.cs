using UnityEngine;

public class HealthChangeHandler : BaseChangeHandler
{
    [SerializeField] private Health _linkedHealth;

    protected override void Start()
    {
        base.Start();

        _linkedHealth.MaximumHealth = _characteristics.CalculatedStamina;
        _linkedHealth.Reset();
    }

    protected override void DoChange()
    {
        _linkedHealth.MaximumHealth = _characteristics.CalculatedStamina;
    }
}

using UnityEngine;

public class SpeedChangeHandler : BaseChangeHandler
{
    [SerializeField] private RigidbodyMovement _movement;

    protected override void Start()
    {
        base.Start();

        _movement.MoveSpeed = _characteristics.CalculatedSpeed;
    }

    protected override void DoChange()
    {
        _movement.MoveSpeed = _characteristics.CalculatedSpeed;
    }
}

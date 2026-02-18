using System;

public interface INotifyAvailabilityChanged
{
    bool State { get; }
    event Action OnTrue;
    event Action OnFalse;
}
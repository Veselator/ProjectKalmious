using System;
using UnityEngine;

public class TrackAbilitiesPointsChanged : MonoBehaviour, INotifyAvailabilityChanged
{
    [SerializeField] private AvailableAbilitiesManager _availableAbilitiesManager;

    private bool _state;

    public bool State => _state;
    public event Action OnTrue;
    public event Action OnFalse;

    private void OnEnable()
    {
        _availableAbilitiesManager.OnPointsChanged += HandlePointsChanged;

        HandlePointsChanged(_availableAbilitiesManager.AvailablePoints);
    }

    private void OnDisable()
    {
        _availableAbilitiesManager.OnPointsChanged -= HandlePointsChanged;
    }

    private void HandlePointsChanged(int points)
    {
        bool newState = _availableAbilitiesManager.IsAnythingAffordable();
        if (newState == _state) return;

        _state = newState;
        if (_state) OnTrue?.Invoke();
        else OnFalse?.Invoke();
    }
}
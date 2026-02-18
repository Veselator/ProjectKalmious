using System;
using UnityEngine;

public class TrackCharsPointsChanged : MonoBehaviour, INotifyAvailabilityChanged
{
    [SerializeField] private CharacteristicsHandler _characteristicsHandler;

    private bool _state;

    public bool State => _state;
    public event Action OnTrue;
    public event Action OnFalse;

    private void OnEnable()
    {
        _characteristicsHandler.OnPointsChanged += HandlePointsChanged;

        HandlePointsChanged(_characteristicsHandler.PointsToAdd);
    }

    private void OnDisable()
    {
        _characteristicsHandler.OnPointsChanged -= HandlePointsChanged;
    }

    private void HandlePointsChanged(int points)
    {
        bool newState = points > 0;
        if (newState == _state) return;

        _state = newState;
        if (_state) OnTrue?.Invoke();
        else OnFalse?.Invoke();
    }
}
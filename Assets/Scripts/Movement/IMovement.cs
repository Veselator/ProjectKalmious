using System;
using UnityEngine;

public interface IMovement
{
    void Move(Vector2 direction);
    void Stop();

    public event Action OnMoveStarted;
    public event Action OnMoveStopped;
    public event Action<Vector2> OnDirectionChanged;
}
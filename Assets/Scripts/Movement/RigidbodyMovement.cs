using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidbodyMovement : MonoBehaviour, IMovement
{
    public float MoveSpeed = 5f;

    private Vector2 _externalVelocity;
    private Rigidbody2D _rigidbody;
    private Vector2 _currentDirection;
    private bool _isMoving;

    public event Action OnMoveStarted;
    public event Action OnMoveStopped;
    public event Action<Vector2> OnDirectionChanged;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        if (direction != _currentDirection)
        {
            _currentDirection = direction;
            OnDirectionChanged?.Invoke(direction);
        }

        bool wasMoving = _isMoving;
        _isMoving = direction.sqrMagnitude > 0.1f;

        if (_isMoving && !wasMoving)
        {
            OnMoveStarted?.Invoke();
        }
        else if (!_isMoving && wasMoving)
        {
            OnMoveStopped?.Invoke();
        }
    }

    public void AddExternalVelocity(Vector2 velocity)
    {
        _externalVelocity += velocity;
    }

    public void Stop()
    {
        Move(Vector2.zero);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = Vector2.zero;

        Vector2 displacement = _externalVelocity * Time.fixedDeltaTime;

        if (_isMoving)
            displacement += _currentDirection * MoveSpeed * Time.fixedDeltaTime;

        _rigidbody.MovePosition(_rigidbody.position + displacement);

        if (_externalVelocity == Vector2.zero) return;

        _externalVelocity = Vector2.Lerp(_externalVelocity, Vector2.zero, 10f * Time.fixedDeltaTime);
        if (_externalVelocity.sqrMagnitude < 0.01f)
            _externalVelocity = Vector2.zero;
    }
}
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidbodyMovement : MonoBehaviour, IMovement
{
    [SerializeField] private float _moveSpeed = 5f;

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
        _isMoving = direction.sqrMagnitude > 0.01f;

        if (_isMoving && !wasMoving)
        {
            OnMoveStarted?.Invoke();
        }
        else if (!_isMoving && wasMoving)
        {
            OnMoveStopped?.Invoke();
        }
    }

    public void Stop()
    {
        Move(Vector2.zero);
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            Vector2 newPosition = _rigidbody.position + _currentDirection * _moveSpeed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(newPosition);
        }
    }
}
using UnityEngine;

[RequireComponent(typeof(RigidbodyMovement))]
public class PlayerInputHandler : MonoBehaviour
{
    private RigidbodyMovement _movement;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _movement = GetComponent<RigidbodyMovement>();
    }

    private void Start()
    {
        _playerInput = PlayerInput.Instance;
    }

    private void Update()
    {
        Vector2 inputDirection = _playerInput.GetMovementVector();
        _movement.Move(inputDirection);
    }
}
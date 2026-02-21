using UnityEngine;

[RequireComponent(typeof(RigidbodyMovement))]
public class PlayerInputHandler : MonoBehaviour
{
    // Класс, который обрабатывает ввод игрока и отдаёт команды RigidvodyMovement

    private RigidbodyMovement _movement;
    private PlayerInput _playerInput;
    private GlobalFlags _globalFlags;

    private bool _isActive = true;

    private void Awake()
    {
        _movement = GetComponent<RigidbodyMovement>();
    }

    private void Start()
    {
        _playerInput = PlayerInput.Instance;
        _globalFlags = GlobalFlags.Instance;

        _globalFlags.OnGameOver += HandleGameOver;
    }

    private void OnDestroy()
    {
        _globalFlags.OnGameOver -= HandleGameOver;
    }

    private void HandleGameOver()
    {
        _isActive = false;
        _movement.Stop();
    }

    private void Update()
    {
        if (!_isActive) return;

        Vector2 inputDirection = _playerInput.GetMovementVector();
        _movement.Move(inputDirection);
    }
}
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimations : MonoBehaviour
{
    [SerializeField] private RigidbodyMovement _movement;
    [SerializeField] private Health _health;
    [SerializeField] private Animator _animator;

    [SerializeField] private string _idleAnimationName = "Idle";
    [SerializeField] private string _moveAnimationName = "Move";
    [SerializeField] private string _damageAnimationName = "Damage";
    [SerializeField] private string _deathAnimationName = "Death";

    private int _idleHash;
    private int _moveHash;
    private int _damageHash;
    private int _deathHash;

    private void Awake()
    {
        _idleHash = Animator.StringToHash(_idleAnimationName);
        _moveHash = Animator.StringToHash(_moveAnimationName);
        _damageHash = Animator.StringToHash(_damageAnimationName);
        _deathHash = Animator.StringToHash(_deathAnimationName);

        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
    }

    private void OnEnable()
    {
        if (_movement != null)
        {
            _movement.OnMoveStarted += HandleMoveStarted;
            _movement.OnMoveStopped += HandleMoveStopped;
        }

        if (_health != null)
        {
            _health.OnDamaged += HandleDamaged;
            _health.OnDeath += HandleDeath;
        }
    }

    private void OnDisable()
    {
        if (_movement != null)
        {
            _movement.OnMoveStarted -= HandleMoveStarted;
            _movement.OnMoveStopped -= HandleMoveStopped;
        }

        if (_health != null)
        {
            _health.OnDamaged -= HandleDamaged;
            _health.OnDeath -= HandleDeath;
        }
    }

    private void HandleMoveStarted()
    {
        _animator.Play(_moveHash);
    }

    private void HandleMoveStopped()
    {
        _animator.Play(_idleHash);
    }

    private void HandleDamaged(float damage)
    {
        _animator.Play(_damageHash);
    }

    private void HandleDeath()
    {
        _animator.Play(_deathHash);
    }
}
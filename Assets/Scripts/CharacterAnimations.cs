using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimations : MonoBehaviour
{
    [SerializeField] private RigidbodyMovement _movement;
    [SerializeField] private Health _health;
    [SerializeField] private Animator _animator;

    [SerializeField] private string _walkingParameterName = "IsWalking";
    [SerializeField] private string _damageTriggerName = "Damage";
    [SerializeField] private string _deathTriggerName = "Death";

    private int _walkingHash;
    private int _damageHash;
    private int _deathHash;

    private void Awake()
    {
        _walkingHash = Animator.StringToHash(_walkingParameterName);
        _damageHash = Animator.StringToHash(_damageTriggerName);
        _deathHash = Animator.StringToHash(_deathTriggerName);

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
            if(!string.IsNullOrEmpty(_damageTriggerName)) _health.OnDamaged += HandleDamaged;
            if (!string.IsNullOrEmpty(_deathTriggerName)) _health.OnDeath += HandleDeath;
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
            if (!string.IsNullOrEmpty(_damageTriggerName)) _health.OnDamaged -= HandleDamaged;
            if (!string.IsNullOrEmpty(_deathTriggerName)) _health.OnDeath -= HandleDeath;
        }
    }

    private void HandleMoveStarted()
    {
        _animator.SetBool(_walkingHash, true);
    }

    private void HandleMoveStopped()
    {
        _animator.SetBool(_walkingHash, false);
    }

    private void HandleDamaged(float damage, Collider2D _)
    {
        _animator.SetTrigger(_damageHash);
    }

    private void HandleDeath()
    {
        _animator.SetTrigger(_deathHash);
    }
}
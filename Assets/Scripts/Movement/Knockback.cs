using UnityEngine;
using System.Collections;

public class Knockback : MonoBehaviour
{
    [SerializeField] private RigidbodyMovement _movement;
    [SerializeField] private Health _health;

    [SerializeField] private float _resistance = 0.4f;
    private const float _damageFactor = 1f;

    private void Start()
    {
        if(_movement == null) _movement = GetComponent<RigidbodyMovement>();
        if(_health == null) _health = GetComponent<Health>();

        _health.OnDamaged += HandleKnockback;
    }

    private void HandleKnockback(float damage, Collider2D source)
    {
        if (_resistance >= 1f) return;
        float force = damage * (1f - _resistance) * _damageFactor;
        Vector2 direction = (transform.position - source.transform.position).normalized;
        _movement.AddExternalVelocity(direction * force);
    }
}

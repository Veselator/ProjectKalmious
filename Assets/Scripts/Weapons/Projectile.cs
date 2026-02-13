using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Projectile : MonoBehaviour
{
    private LayerMask _targetLayers;
    [SerializeField] private float _secondsToLive = 5f;
    private ProjectilePool _pool;

    private Damage _damage;
    private float _speed;
    private Vector3 _direction;
    private float _currentTime = 0f;

    private Collider2D _collider;

    public event Action<Collider2D> OnProjectileHit;

    public void Initialize(Damage damage, float speed, Vector3 direction, LayerMask targetLayers, ProjectilePool pool)
    {
        _damage = damage;
        _speed = speed;
        _direction = direction.normalized;
        _targetLayers = targetLayers;
        _pool = pool;
        _currentTime = 0f;
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        HandlePositionChange();
        HandleTime();
    }

    private void HandlePositionChange()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }

    private void HandleTime()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > _secondsToLive)
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsInTargetLayer(collision.gameObject.layer))
        {
            return;
        }

        Health targetHealth = collision.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(_damage, _collider);
        }

        DestroyProjectile(collision);
    }

    private bool IsInTargetLayer(int layer)
    {
        return (_targetLayers.value & (1 << layer)) != 0;
    }

    private void DestroyProjectile(Collider2D colli = null)
    {
        OnProjectileHit?.Invoke(colli);
        if (_pool != null) _pool.Return(gameObject);
        else Destroy(gameObject);
    }
}
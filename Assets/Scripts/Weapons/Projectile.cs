using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected LayerMask _targetLayers;
    [SerializeField] protected float _secondsToLive = 5f;
    protected ProjectilePool _pool;
    protected Damage _damage;
    protected float _speed;
    protected Vector3 _direction;
    protected float _currentTime = 0f;
    protected Collider2D _collider;
    private bool _isHitted;

    public event Action<Collider2D> OnProjectileHit;

    public virtual void Initialize(Damage damage, float speed, Vector3 direction, LayerMask targetLayers, ProjectilePool pool)
    {
        _damage = damage;
        _speed = speed;
        _direction = direction.normalized;
        _targetLayers = targetLayers;
        _pool = pool;
        _currentTime = 0f;
        _collider = GetComponent<Collider2D>();
        _isHitted = false;
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

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isHitted) return;
        if (!IsInTargetLayer(collision.gameObject.layer)) return;

        _isHitted = true;

        Health targetHealth = collision.GetComponent<Health>();
        if (targetHealth != null) targetHealth.TakeDamage(_damage, _collider);

        DestroyProjectile(collision);
    }

    protected bool IsInTargetLayer(int layer)
    {
        return (_targetLayers.value & (1 << layer)) != 0;
    }

    protected void DestroyProjectile(Collider2D colli = null)
    {
        OnProjectileHit?.Invoke(colli);
        if (_pool != null) _pool.Return(gameObject);
        else Destroy(gameObject);
    }
}
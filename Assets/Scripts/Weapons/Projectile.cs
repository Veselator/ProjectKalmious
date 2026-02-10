using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private LayerMask _targetLayers;
    [SerializeField] private float _secondsToLive = 5f;

    private Damage _damage;
    private float _speed;
    private Vector3 _direction;
    private float _currentTime = 0f;

    public event Action<Collider2D> OnProjectileHit;

    public void Initialize(Damage damage, float speed, Vector3 direction, LayerMask targetLayers)
    {
        _damage = damage;
        _speed = speed;
        _direction = direction.normalized;
        _targetLayers = targetLayers;
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
            targetHealth.TakeDamage(_damage);
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
        Destroy(gameObject);
    }
}
using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayers;

    private Damage _damage;
    private float _speed;
    private Vector3 _direction;

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
        transform.position += _direction * _speed * Time.deltaTime;
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

        OnProjectileHit?.Invoke(collision);
        Destroy(gameObject);
    }

    private bool IsInTargetLayer(int layer)
    {
        return (_targetLayers.value & (1 << layer)) != 0;
    }
}
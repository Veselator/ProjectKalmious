using UnityEngine;

public class BouncingProjectile : Projectile
{
    [SerializeField] private int _maxBounces = 3;
    [SerializeField] private float _damageMultiplierPerBounce = 0.7f;
    [SerializeField] private float _bounceSearchRadius = 10f;

    private int _currentBounces;

    public override void Initialize(Damage damage, float speed, Vector3 direction, LayerMask targetLayers, ProjectilePool pool)
    {
        base.Initialize(damage, speed, direction, targetLayers, pool);
        _currentBounces = 0;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsInTargetLayer(collision.gameObject.layer)) return;

        Health targetHealth = collision.GetComponent<Health>();
        if (targetHealth != null) targetHealth.TakeDamage(_damage, _collider);

        _currentBounces++;

        if (_currentBounces >= _maxBounces)
        {
            DestroyProjectile(collision);
            return;
        }

        _damage = _damage * _damageMultiplierPerBounce;

        Transform nextTarget = FindNextTarget(collision);
        if (nextTarget == null)
        {
            DestroyProjectile(collision);
            return;
        }

        _direction = (nextTarget.position - transform.position).normalized;
    }

    private Transform FindNextTarget(Collider2D hitCollider)
    {
        Collider2D[] candidates = Physics2D.OverlapCircleAll(transform.position, _bounceSearchRadius, _targetLayers);

        float closestDistance = float.MaxValue;
        Transform closest = null;

        foreach (Collider2D candidate in candidates)
        {
            if (candidate == hitCollider) continue;

            Health health = candidate.GetComponent<Health>();
            if (health == null || health.IsDied) continue;

            float distance = Vector2.Distance(transform.position, candidate.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = candidate.transform;
            }
        }

        return closest;
    }
}
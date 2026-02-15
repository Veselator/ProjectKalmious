using UnityEngine;

public class StrongPunch : BaseAbility
{
    [SerializeField] private float _radius = 5f;
    [SerializeField] private Damage _damage;
    [SerializeField] private LayerMask _targetLayer;

    protected override void Act()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(_ownerTransform.position, _radius, _targetLayer);

        foreach (Collider2D hit in hits)
        {
            Health health = hit.GetComponent<Health>();
            if (health != null) health.TakeDamage(_damage * (2f / Vector2.Distance(_ownerTransform.position, hit.transform.position)), _ownerCollider);
        }
    }
}
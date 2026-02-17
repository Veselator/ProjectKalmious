using UnityEngine;
using System;
using System.Collections.Generic;

public class MeleeWeapon : BaseWeapon
{
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private float _attackAngle = 90f;
    [SerializeField] private int _maxTargets = 5;

    public event Action OnSwing;
    public event Action<Health> OnTargetHit;

    public float AttackAngle => _attackAngle;

    public override void Act()
    {
        if (!CanAct()) return;

        StartAct();
        PerformMeleeAttack();
        CompleteAct();
    }

    private void PerformMeleeAttack()
    {
        OnSwing?.Invoke();

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _attackRange, _targetLayer);

        int targetsHit = 0;
        foreach (Collider2D collider in hitColliders)
        {
            if (targetsHit >= _maxTargets) break;

            if (IsInAttackAngle(collider.transform.position, transform.position))
            {
                Health targetHealth = collider.GetComponent<Health>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(_damage, _collider);
                    OnTargetHit?.Invoke(targetHealth);
                    targetsHit++;
                }
            }
        }
    }

    private bool IsInAttackAngle(Vector3 targetPosition, Vector3 attackPosition)
    {
        Vector3 directionToTarget = (targetPosition - attackPosition).normalized;
        Vector3 attackDirection = transform.right;

        float angle = Vector3.Angle(attackDirection, directionToTarget);
        return angle <= _attackAngle / 2f;
    }
}
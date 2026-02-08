using UnityEngine;
using System;

public class MeleeWeapon : BaseWeapon
{
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private float _attackAngle = 90f;
    [SerializeField] private int _maxTargets = 5;

    public event Action OnSwing;
    public event Action<Health> OnTargetHit;

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

        Vector3 attackPosition = _attackPoint != null ? _attackPoint.position : transform.position;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPosition, _attackRange, _targetLayer);

        int targetsHit = 0;
        foreach (Collider2D collider in hitColliders)
        {
            if (targetsHit >= _maxTargets) break;

            if (IsInAttackAngle(collider.transform.position, attackPosition))
            {
                Health targetHealth = collider.GetComponent<Health>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(_damage);
                    OnTargetHit?.Invoke(targetHealth);
                    targetsHit++;
                }
            }
        }
    }

    private bool IsInAttackAngle(Vector3 targetPosition, Vector3 attackPosition)
    {
        Vector3 directionToTarget = (targetPosition - attackPosition).normalized;
        Vector3 attackDirection = _attackPoint != null ? _attackPoint.right : transform.right;

        float angle = Vector3.Angle(attackDirection, directionToTarget);
        return angle <= _attackAngle / 2f;
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);

        Vector3 rightBoundary = Quaternion.Euler(0, 0, -_attackAngle / 2f) * _attackPoint.right * _attackRange;
        Vector3 leftBoundary = Quaternion.Euler(0, 0, _attackAngle / 2f) * _attackPoint.right * _attackRange;

        Gizmos.DrawLine(_attackPoint.position, _attackPoint.position + rightBoundary);
        Gizmos.DrawLine(_attackPoint.position, _attackPoint.position + leftBoundary);
    }
}
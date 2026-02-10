using System.Collections;
using UnityEngine;

public class MeleeAttackAnimation : BaseWeaponAttackAnimation
{
    [SerializeField] private AnimationCurve _curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [SerializeField] private TrailRenderer _trail;

    private MeleeWeapon _melee;
    private float startRotation, endRotation;

    protected override void Init()
    {
        _trail.emitting = false;
        _melee = _linkedWeapon as MeleeWeapon;
        float _totalAngle = _melee != null ? _melee.AttackAngle : 90f;
        float halfAngle = _totalAngle / 2f;

        startRotation = -halfAngle;
        endRotation = halfAngle;
    }

    protected override IEnumerator AttackAnimation()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, startRotation);

        _trail.emitting = true;

        float elapsed = 0f;
        while (elapsed < _animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _animationDuration);
            float angle = Mathf.Lerp(startRotation, endRotation, _curve.Evaluate(t));
            transform.localRotation = Quaternion.Euler(0f, 0f, angle);
            yield return null;
        }

        transform.localRotation = Quaternion.Euler(0f, 0f, endRotation);

        _trail.emitting = false;

        transform.localRotation = Quaternion.identity;
    }
}
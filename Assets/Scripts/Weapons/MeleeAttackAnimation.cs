using System.Collections;
using UnityEngine;

public class MeleeAttackAnimation : BaseWeaponAttackAnimation
{
    // Анимация для атаки ближнего боя

    [SerializeField] private AnimationCurve _curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [SerializeField] private TrailRenderer _trail;

    private MeleeWeapon _melee;
    private float startRotation, endRotation;
    private float _halfLife; // Типо половина длительности)

    protected override void Init()
    {
        _trail.emitting = false;
        _melee = _linkedWeapon as MeleeWeapon;

        _halfLife = _animationDuration / 2;

        float rotationZ = _linkedWeapon.gameObject.transform.rotation.z;
        float totalAngle = _melee.AttackAngle + rotationZ;
        float halfAngle = totalAngle / 2f;

        startRotation = -halfAngle;
        endRotation = halfAngle;
    }

    protected override IEnumerator AttackAnimation()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, startRotation);

        _trail.emitting = true;

        float elapsed = 0f;
        while (elapsed < _halfLife)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _halfLife);
            float angle = Mathf.Lerp(startRotation, endRotation, _curve.Evaluate(t));
            transform.localRotation = Quaternion.Euler(0f, 0f, angle);
            yield return null;
        }

        transform.localRotation = Quaternion.Euler(0f, 0f, endRotation);

        _trail.emitting = false;

        elapsed = 0f;
        while (elapsed < _halfLife)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _halfLife);
            float angle = Mathf.Lerp(endRotation, startRotation, _curve.Evaluate(t));
            transform.localRotation = Quaternion.Euler(0f, 0f, angle);
            yield return null;
        }

        transform.localRotation = Quaternion.Euler(0f, 0f, startRotation);
    }
}
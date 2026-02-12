using System.Collections;
using UnityEngine;

public class MeleeAttackAnimation : BaseWeaponAttackAnimation
{
    [SerializeField] private AnimationCurve _curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [SerializeField] private TrailRenderer _trail;

    private MeleeWeapon _melee;
    private float _left, _right, _origin;
    private float _thirdDuration;

    protected override void Init()
    {
        _trail.emitting = false;
        _melee = _linkedWeapon as MeleeWeapon;

        _thirdDuration = _animationDuration / 3f;

        _origin = transform.localRotation.eulerAngles.z;

        float halfAngle = _melee.AttackAngle / 2f;
        _right = _origin + halfAngle;
        _left = _origin - halfAngle;
    }

    protected override IEnumerator AttackAnimation()
    {
        _trail.emitting = true;

        yield return RotateTo(_origin, _right, _thirdDuration);
        yield return RotateTo(_right, _left, _thirdDuration);

        _trail.emitting = false;

        yield return RotateTo(_left, _origin, _thirdDuration);
    }

    private IEnumerator RotateTo(float from, float to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float angle = Mathf.Lerp(from, to, _curve.Evaluate(t));
            transform.localRotation = Quaternion.Euler(0f, 0f, angle);
            yield return null;
        }
        transform.localRotation = Quaternion.Euler(0f, 0f, to);
    }
}
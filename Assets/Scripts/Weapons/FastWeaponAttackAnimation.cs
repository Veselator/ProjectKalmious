using System.Collections;

public class FastWeaponAttackAnimation : CommonAttackAnimation
{
    protected override IEnumerator AttackAnimation()
    {
        yield break;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (_linkedWeapon == null) return;

        _linkedWeapon.OnActStarted -= OnActStarted;
        _linkedWeapon.OnActCompleted -= OnActCompleted;
    }

    protected override void Init()
    {
        _linkedWeapon.OnActStarted += OnActStarted;
        _linkedWeapon.OnActCompleted += OnActCompleted;
    }

    private void OnActStarted()
    {
        _linkedAnimator.SetBool(_animationString, true);
    }

    private void OnActCompleted()
    {
        _linkedAnimator.SetBool(_animationString, false);
    }
}
